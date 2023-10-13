namespace DevExpress.UITemplates.Collection.Utilities {
    using System;
    using System.Linq;

    public static class Password {
        public enum Strength {
            Blank,
            VeryWeak,
            Weak,
            Medium,
            Strong,
            VeryStrong
        }
        [Flags]
        public enum Requirements {
            None = 0,
            Digits = 1,
            Letters = 2,
            Capitals = 4,
            SpecialSymbols = 8,
            Length = 16
        }
        public static Strength Check(string password) {
            Requirements requirements;
            return Check(password, out requirements);
        }
        public static Strength Check(string password, out Requirements requirements) {
            requirements = Requirements.None;
            if(string.IsNullOrEmpty(password))
                return Strength.Blank;
            if(password.Length < 4)
                return Strength.VeryWeak;
            int score = 1;
            if(password.Length >= 8) {
                requirements |= Requirements.Length;
                score++;
            }
            if(password.Length >= 12)
                score++;
            if(password.Any(char.IsDigit)) {
                requirements |= Requirements.Digits;
                score++;
            }
            if(password.Any(char.IsLetter))
                requirements |= Requirements.Letters;
            if(password.Any(char.IsLower) && password.Any(char.IsUpper)) {
                requirements |= Requirements.Capitals;
                score++;
            }
            if(password.Any(IsSpecialSymbol)) {
                requirements |= Requirements.SpecialSymbols;
                score++;
            }
            return (Strength)(Math.Min(5, score));
        }
        static bool IsSpecialSymbol(char c) {
            int code = (int)c;
            if(code >= 32 && code <= 47)
                return true;
            if(code >= 58 && code <= 64)
                return true;
            if(code >= 91 && code <= 96)
                return true;
            if(code >= 123 && code <= 126)
                return true;
            return false;
        }
    }
}
