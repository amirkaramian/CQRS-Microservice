﻿using System;
using System.Text;

namespace Payscrow.ProjectMilestone.Application.Common.Helpers
{
    public static class RandomHelper
    {
        /// <summary>
        /// Common method for creating an instance of System.Random, initialized with Guid.NewGuid().GetHashCode() 
        /// as the seed.
        /// </summary>
        public static Random CreateRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }


        // Generates a random string with a given size.    
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)CreateRandom().Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string GenerateUniqueNumbers()
        {
            long s1 = CreateRandom().Next(000000, 999999);
            long s2 = Convert.ToInt64(DateTime.Now.ToString("ddMMyyyyHHmmss"));
            return s1.ToString() + "" + s2.ToString();
        }
    }

}
