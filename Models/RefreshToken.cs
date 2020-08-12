using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using App_NetCore.Data;
using Microsoft.EntityFrameworkCore;

namespace NetCoreApi.Models{
    public class RefreshToken{
        public Guid Id {get; set;}
        public string Token {get; set;}
        public DateTime Expires {get; set;}
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created {get; set;}

        public string CreatedByIp {get; set;}
        public DateTime? Revoked {get; set;}
        public string RevokedByIp {get; set;}
        public string ReplacedByToken {get; set;}
        public bool IsActive => Revoked == null && !IsExpired;

        public string UserId {get; set;}
        public ApplicationUser User {get; set;}

        public static RefreshToken GenerateRefreshToken (string ipAddress){
            using (var rng = new RNGCryptoServiceProvider()){
                var randomBytes = new byte[64];
                rng.GetBytes(randomBytes);
                return new RefreshToken{
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public static RefreshToken Refresh(string ipAddress, string token, ApplicationDbContext context, out ApplicationUser user){
            user = context.Set<ApplicationUser>().Include(e => e.RefreshTokens).FirstOrDefault(s => s.RefreshTokens.Any(t => t.Token == token));
            if (user == null) return null;
            var RefreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            if (!RefreshToken.IsActive) return null;
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            RefreshToken.Revoked = DateTime.UtcNow;
            RefreshToken.RevokedByIp = ipAddress;
            RefreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            context.Update(user);
            context.SaveChanges();
            return newRefreshToken;

        }
    }



}