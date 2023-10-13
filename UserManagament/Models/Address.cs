namespace DevExpress.UITemplates.Collection.Models {
    using System.ComponentModel.DataAnnotations;

    public class Address : Entity<long> {
        [Display(Name = "Address")]
        public string Line { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override bool Equals(object obj) {
            Address a = obj as Address;
            if(ReferenceEquals(a, null) || !base.Equals(a))
                return false;
            return Line == a.Line && City == a.City && Latitude == a.Latitude && Longitude == a.Longitude;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override string ToString() {
            return Line + ", " + City;
        }
        public override void Assign(Entity<long> source) {
            base.Assign(source);
            var addressSource = source as Address;
            if(addressSource != null) {
                Line = addressSource.Line;
                City = addressSource.City;
                Latitude = addressSource.Latitude;
                Longitude = addressSource.Longitude;
            }
        }
    }
    public class USAddress : Address {
        public USState State { get; set; }
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }
        public override bool Equals(object obj) {
            USAddress us = obj as USAddress;
            if(ReferenceEquals(us, null) || !base.Equals(obj))
                return false;
            return ZipCode == us.ZipCode && State == us.State;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override void Assign(Entity<long> source) {
            base.Assign(source);
            var usSource = source as USAddress;
            if(usSource != null) {
                State = usSource.State;
                ZipCode = usSource.ZipCode;
            }
        }
    }
    //
    public enum USState {
        CA = 0,
        AR = 1,
        AL = 2,
        AK = 3,
        AZ = 4,
        CO = 5,
        CT = 6,
        DE = 7,
        DC = 8,
        FL = 9,
        GA = 10,
        HI = 11,
        ID = 12,
        IL = 13,
        IN = 14,
        IA = 15,
        KS = 16,
        KY = 17,
        LA = 18,
        ME = 19,
        MD = 20,
        MA = 21,
        MI = 22,
        MN = 23,
        MS = 24,
        MO = 25,
        MT = 26,
        NE = 27,
        NV = 28,
        NH = 29,
        NJ = 30,
        NM = 31,
        NY = 32,
        NC = 33,
        OH = 34,
        OK = 35,
        OR = 36,
        PA = 37,
        RI = 38,
        SC = 39,
        SD = 40,
        TN = 41,
        TX = 42,
        UT = 43,
        VT = 44,
        VA = 45,
        WA = 46,
        WV = 47,
        WI = 48,
        WY = 49,
        ND = 50
    }
}
