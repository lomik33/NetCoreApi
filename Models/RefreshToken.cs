using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
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
    }



}