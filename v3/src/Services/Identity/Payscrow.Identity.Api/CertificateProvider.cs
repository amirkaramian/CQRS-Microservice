//using CertificateManager;
//using CertificateManager.Models;
//using System;
//using System.Collections.Generic;
//using System.Security.Cryptography;
//using System.Security.Cryptography.X509Certificates;

//namespace Payscrow.Identity.Api
//{
//    public static class CertificateProvider
//    {
//        private static readonly CreateCertificates _cc;

//        public static X509Certificate2 CreateRsaCertificate(string dnsName, int validityPeriodInYears)
//        {
//            var basicConstraints = new BasicConstraints
//            {
//                CertificateAuthority = false,
//                HasPathLengthConstraint = false,
//                PathLengthConstraint = 0,
//                Critical = false
//            };

//            var subjectAlternativeName = new SubjectAlternativeName
//            {
//                DnsName = new List<string>
//                {
//                    dnsName,
//                }
//            };

//            var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

//            // only if certification authentication is used
//            var enhancedKeyUsages = new OidCollection
//            {
//                OidLookup.ClientAuthentication,
//                OidLookup.ServerAuthentication
//                // OidLookup.CodeSigning,
//                // OidLookup.SecureEmail,
//                // OidLookup.TimeStamping
//            };

//            var certificate = _cc.NewRsaSelfSignedCertificate(
//                new DistinguishedName { CommonName = dnsName },
//                basicConstraints,
//                new ValidityPeriod
//                {
//                    ValidFrom = DateTimeOffset.UtcNow,
//                    ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
//                },
//                subjectAlternativeName,
//                enhancedKeyUsages,
//                x509KeyUsageFlags,
//                new RsaConfiguration
//                {
//                    KeySize = 2048,
//                    HashAlgorithmName = HashAlgorithmName.SHA512
//                });

//            return certificate;
//        }

//        public static X509Certificate2 CreateECDsaCertificate(string dnsName, int validityPeriodInYears)
//        {
//            var basicConstraints = new BasicConstraints
//            {
//                CertificateAuthority = false,
//                HasPathLengthConstraint = false,
//                PathLengthConstraint = 0,
//                Critical = false
//            };

//            var subjectAlternativeName = new SubjectAlternativeName
//            {
//                DnsName = new List<string>
//                {
//                    dnsName,
//                }
//            };

//            var x509KeyUsageFlags = X509KeyUsageFlags.DigitalSignature;

//            // only if certification authentication is used
//            var enhancedKeyUsages = new OidCollection {
//                OidLookup.ClientAuthentication,
//                OidLookup.ServerAuthentication
//                // OidLookup.CodeSigning,
//                // OidLookup.SecureEmail,
//                // OidLookup.TimeStamping
//            };

//            var certificate = _cc.NewECDsaSelfSignedCertificate(
//                new DistinguishedName { CommonName = dnsName },
//                basicConstraints,
//                new ValidityPeriod
//                {
//                    ValidFrom = DateTimeOffset.UtcNow,
//                    ValidTo = DateTimeOffset.UtcNow.AddYears(validityPeriodInYears)
//                },
//                subjectAlternativeName,
//                enhancedKeyUsages,
//                x509KeyUsageFlags,
//                new ECDsaConfiguration
//                {
//                    KeySize = 384,
//                    HashAlgorithmName = HashAlgorithmName.SHA384
//                });

//            return certificate;
//        }
//    }
//}