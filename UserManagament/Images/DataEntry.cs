namespace DevExpress.UITemplates.Collection.Images {
    using System;
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;

    public static class DataEntry {
        const string assetsRoot = "UserManagament.Assets."; 
        [ThreadStatic]
        static SvgImageCollection svgImagesCore;
        public static SvgImageCollection SvgImages {
            get { return svgImagesCore ?? (svgImagesCore = SvgImageCollection.FromResources(assetsRoot + "SVG.DataEntry", typeof(DataEntry).Assembly)); }
        }
        public static SvgImage Email {
            get { return SvgImages[nameof(Email)]; }
        }
        public static SvgImage Key {
            get { return SvgImages[nameof(Key)]; }
        }
        public static SvgImage Phone {
            get { return SvgImages[nameof(Phone)]; }
        }
        public static SvgImage World {
            get { return SvgImages[nameof(World)]; }
        }
        public static SvgImage Card {
            get { return SvgImages[nameof(Card)]; }
        }
        public static SvgImage Employees {
            get { return SvgImages[nameof(Employees)]; }
        }
        public static SvgImage Customers {
            get { return SvgImages[nameof(Customers)]; }
        }
        public static SvgImage Products {
            get { return SvgImages[nameof(Products)]; }
        }
        public static SvgImage Sales {
            get { return SvgImages[nameof(Sales)]; }
        }
        public static SvgImage Tasks {
            get { return SvgImages[nameof(Tasks)]; }
        }
        public static SvgImage Oppurtunities {
            get { return SvgImages[nameof(Oppurtunities)]; }
        }
    }
}
