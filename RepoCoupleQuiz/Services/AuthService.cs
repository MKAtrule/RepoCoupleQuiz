using Microsoft.IdentityModel.Tokens;
using RepoCoupleQuiz.Common.File.Interface;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RepoCoupleQuiz.Services
{
    public class AuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IFileUpload uploadRepo;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IConfiguration config;
        public AuthService(IAuthRepository authRepository, IFileUpload uploadRepo, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IConfiguration config)
        {
            this.authRepository = authRepository;
            this.uploadRepo = uploadRepo;
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
            this.config = config;
        }

        public async Task<AuthCreateResponseDTO> CreateUserAsync(AuthCreateRequestDTO request)
        {
            var emailExists = await authRepository.FindByEmailAsync(request.Email);
            if (emailExists == null)
            {
                if (request.ProfileImage != null && request.ProfileImage.Length > 0)
                {
                    var imagepath = await uploadRepo.UploadImageAsync(request.ProfileImage);
                    var user = new User()
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Password = request.Password,
                        Age = request.Age,
                        Gender = request.Gender,
                        ProfileImage = imagepath,
                        CreatedAt = System.DateTime.Now,
                        Active = true,
                    };
                    var newUser = await authRepository.Create(user);
                    var defaultRole = await roleRepository.GetByName();
                    if (defaultRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = newUser.GlobalId,
                            RoleId = defaultRole.GlobalId,
                            Active = true,
                            CreatedAt = System.DateTime.Now
                        };
                        await userRoleRepository.Create(userRole);

                    }
                    return new AuthCreateResponseDTO {
                    UserId= newUser.GlobalId,
                    Email= request.Email,
                    Gender= request.Gender,
                    ProfileImage = imagepath,
                    Age= request.Age,           
                    };



                }
                else
                {
                    throw new Exception("Image is Required");
                }
            }
            else
            {
                throw new Exception("User with this Email already Exist");
            }
        }
        public async Task<AuthResponseDTO> LoginAsync(AuthRequestDTO request)
        {
            var user = await authRepository.FindUserAsync(request);
            if (user != null)
            {
                var roles = await userRoleRepository.GetUserRolesAsync(user.GlobalId);

                var token = await GenerateToken(user, roles);
                var refreshToken = GenerateRefreshToken();
                await authRepository.SaveRefreshTokenAsync(user, refreshToken);

                return new AuthResponseDTO
                {
                    JwtToken = token,
                    RefreshToken = refreshToken,
                    Email = user.Email,
                    UserName = user.Name,
                    UserId = user.GlobalId,
                };
            }
            else
            {
                throw new Exception("User not found");
            }


        }
        public async Task<UserPersonalInfoResponeDTO> GetPersonalInfoAsync(Guid id)
        {
            var user = await authRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new Exception("Khan shb nhi mily");
            }
            return new UserPersonalInfoResponeDTO
            {
                UserName = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                Age = user.Age,
                UserId = user.GlobalId,
                Image = user
                .ProfileImage,
            };


        }
        public async Task<UserPersonalInfoResponeDTO> UpdateAsync(UpdateUserRequestDTO request)
        {
            var userExist = await authRepository.GetByIdAsync(request.UserId);
            if (userExist is null)
            {
                throw new Exception("user kaine");
            }
            if (request.Image != null && request.Image.Length > 0)
            {
                var imagepath = await uploadRepo.UploadImageAsync(request.Image);
                userExist.Name = request.UserName;
                userExist.Age = request.Age;
                userExist.Gender = request.Gender;
                userExist.ProfileImage = imagepath;
            }
            var newUser = await authRepository.Update(userExist);
            return new UserPersonalInfoResponeDTO
            {
                UserName = newUser.Name,
                Image = newUser.ProfileImage,
                Age = newUser.Age,
                Gender = newUser.Gender,
                UserId = newUser.GlobalId,
                Email = newUser.Email,
            };


        }
        private async Task<string> GenerateToken(User user, List<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return await Task.FromResult(tokenString);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            var principal = GetPrincipalFromExpiredToken(request.Token);
            var email = principal.FindFirstValue("Email");
            var user = await authRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user == null || user.Email != email || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            var roles = await userRoleRepository.GetUserRolesAsync(user.GlobalId);
            var newToken = await GenerateToken(user, roles);
            //var newRefreshToken = GenerateRefreshToken();
       //     await authRepository.SaveRefreshTokenAsync(user, newRefreshToken);

            return new AuthResponseDTO
            {
                JwtToken = newToken,
                RefreshToken =request.RefreshToken,
            };
        }
        public async Task ForgotPasswordAsync(ForgotPasswordRequestDTO request)
        {
            var user = await authRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("No User Associated with this Email");
            }

            var otp = GenerateOtp();
            await authRepository.SaveResetPasswordOtpAsync(user, otp);

            await SendResetPasswordOtpEmail(user.Email, otp, user.Name);
        }

        public string GenerateOtp()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0) % 10000;
                return Math.Abs(value).ToString("D4");
            }
        }

        private async Task SendResetPasswordOtpEmail(string email, string otp, string name)
        {
            using (MailMessage ms = new MailMessage(config["SMTP:Username"], email))
            {
                ms.Subject = "Couple Quiz - OTP Verification";
                ms.Body = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    color: #333;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #fff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    text-align: center;
                    margin-bottom: 20px;
                }}
                .header h2 {{
                    color: #FC4468;
                }}
                .otp-container {{
                    text-align: center;
                    margin: 20px 0;
                }}
                .otp {{
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #FC4468;
                    color: #fff;
                    text-align: center;
                    border-radius: 5px;
                    font-size: 24px;
                    font-weight: bold;
                }}
                .footer {{
                    margin-top: 20px;
                    text-align: center;
                    font-size: 12px;
                    color: #aaa;
                }}
                .footer a {{
                    color: #007bff;
                    text-decoration: none;
                }}
                .footer a:hover {{
                    text-decoration: underline;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h2>Couple Quiz</h2>
                    <p>OTP Verification</p>
                </div>
                <p>Hi {name},</p>
                <p>You recently requested to verify your account for your Couple Quiz  account. Use the OTP below to proceed:</p>
                <div class='otp-container'>
                    <p class='otp'>{otp}</p>
                </div>
                <p>This OTP is only valid for the next 2 minutes.</p>
                <p>If you did not request an OTP, please ignore this email or <a href='mailto:support@invitationcardmaker.com'>contact support</a> if you have questions.</p>
                <div class='footer'>
                    <p>&copy; {DateTime.Now.Year} Invitation Card Maker. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>";
                ms.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = config["SMTP:Host"];
                    smtp.EnableSsl = true;
                    NetworkCredential crd = new NetworkCredential(config["SMTP:Username"], config["SMTP:Password"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = crd;
                    smtp.Port = int.Parse(config["SMTP:Port"]);
                    await smtp.SendMailAsync(ms);
                }
            }
        }

        public async Task<string> VerifyOtpAsync(VerifyOtpRequestDTO request)
        {
            var user = await authRepository.GetUserByOtpAsync(request.Email, request.Otp);
            if (user == null)
            {
                throw new Exception("Invalid or expired OTP");
            }
            user.IsOTpVerified = true;
            user.ResetPasswordOtp = null;
            user.ResetPasswordOtpExpiryTime = null;

            await authRepository.Update(user);
            return user.Email;
        }
        public async Task<string> ConfirmPassword(ConfirmPasswordRequestDTO request)
        {
            var user= await authRepository.FindByEmailAsync(request.Email);
            if(user.IsOTpVerified==false )
            {
                throw new Exception("Otp not verified");
            }
            user.IsOTpVerified = false;
            user.Email = request.Email;
            user.Password=request.NewPassword;
            var updatedUser = await authRepository.Update(user);
            if (updatedUser != null)
            {
                return "Updated Successfuly";
            }
            else
            {
                throw new Exception("User with that email not found");
            }
        }
        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            User userExist = await authRepository.GetByIdAsync(request.UserId);
            if (userExist == null)
            {
                throw new Exception("User not Found");
            }
            else
            {
                userExist.Password = request.NewPassword;
                var updateUser = await authRepository.Update(userExist);
                return new ResetPasswordResponseDTO
                {
                    UserName = userExist.Name,
                    Email = userExist.Email,
                };
            }
        }




    }

}
