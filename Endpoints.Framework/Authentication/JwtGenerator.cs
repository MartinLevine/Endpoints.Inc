using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Endpoints.Framework.Authentication
{
    public class JwtGenerator
    {
        // 用于产生JwtToken的本体生成器
        private JwtSecurityToken token = null;

        // 生成器基本配置
        private GenerateOptions options = new GenerateOptions();

        public JwtGenerator()
        {

        }

        public JwtGenerator(GenerateOptions options)
        {
            this.options = options;
        }

        public void AddOptions(GenerateOptions options)
        {
            this.options = options;
        }

        public void AddClaims(IEnumerable<Claim> claims)
        {
            options.Claims = claims;
        }

        public void AddIssuer(string issuer)
        {
            options.Iss = issuer;
        }

        public void AddAudience(string audience)
        {
            options.Aud = audience;
        }

        public void AddNotBefore(DateTime notBefore)
        {
            options.Nbf = notBefore;
        }

        public void AddExpires(DateTime expires)
        {
            options.Exp = expires;
        }

        public void AddSecurityKey(string securityKey)
        {
            options.SecurityKey = securityKey;
        }

        public void AddAlgorithm(string securityAlgorithm)
        {
            options.Alg = securityAlgorithm;
        }

        public JwtPayload GetPayload()
        {
            return token.Payload;
        }

        public string Generate()
        {
            if (string.IsNullOrEmpty(options.SecurityKey)) throw new NullReferenceException("SecurityKey is null");
            if (string.IsNullOrEmpty(options.Iss)) throw new NullReferenceException("Issuer is null");
            if (string.IsNullOrEmpty(options.Aud)) throw new NullReferenceException("Audience is null");
            token = new JwtSecurityToken(
                issuer: options.Iss,
                audience: options.Aud,
                claims: options.Claims,
                notBefore: options.Nbf,
                expires: options.Exp,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey)),
                    algorithm: options.Alg
                )
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class GenerateOptions
    {
        /// <summary>
        /// 自定义字段
        /// </summary>
        [DefaultValue(null)]
        public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();

        /// <summary>
        /// 发布者
        /// </summary>
        [DefaultValue("")]
        public string Iss { get; set; } = string.Empty;

        /// <summary>
        /// 接收者
        /// </summary>
        [DefaultValue("")]
        public string Aud { get; set; } = string.Empty;

        /// <summary>
        /// 秘钥
        /// </summary>
        [DefaultValue("")]
        public string SecurityKey { get; set; } = string.Empty;

        /// <summary>
        /// 加密算法
        /// 枚举自类：SecurityAlgorithms
        /// </summary>
        [DefaultValue(typeof(SecurityAlgorithms), SecurityAlgorithms.HmacSha256)]
        public string Alg { get; set; } = SecurityAlgorithms.HmacSha256;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime Nbf { get; set; } = DateTime.Now;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Exp { get; set; } = DateTime.Now.AddMinutes(30);
    }
}
