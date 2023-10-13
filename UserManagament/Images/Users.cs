namespace DevExpress.UITemplates.Collection.Images {
    using System;
    using System.Collections.Concurrent;
    using System.Drawing;
    using DevExpress.Utils;

    public static class Users {
        const string assetsRoot = "UserManagament.Assets."; 
        [ThreadStatic]
        static ImageCollection imagesCore;
        public static ImageCollection Images {
            get { return imagesCore ?? (imagesCore = CreateAllImages()); }
        }
        static ImageCollection CreateAllImages() {
            var imgCollection = new ImageCollection();
            imgCollection.ImageSize = new Size(350, 420);
            var rootAsm = typeof(Users).Assembly;
            var usersRoot = assetsRoot + "PNG.Users.";
            var unknown = ResourceImageHelper.CreateImageFromResources(usersRoot + nameof(Unknown) + ".png", rootAsm);
            var user1 = ResourceImageHelper.CreateImageFromResources(usersRoot + nameof(SampleUser1) + ".png", rootAsm);
            var user2 = ResourceImageHelper.CreateImageFromResources(usersRoot + nameof(SampleUser2) + ".png", rootAsm);
            var user3 = ResourceImageHelper.CreateImageFromResources(usersRoot + nameof(SampleUser3) + ".png", rootAsm);
            imgCollection.AddImage(unknown, nameof(Unknown));
            imgCollection.AddImage(user1, nameof(SampleUser1));
            imgCollection.AddImage(user2, nameof(SampleUser2));
            imgCollection.AddImage(user3, nameof(SampleUser3));
            return imgCollection;
        }
        public static Image Unknown {
            get { return Images.Images[nameof(Unknown)]; }
        }
        public static Image SampleUser1 {
            get { return Images.Images[nameof(SampleUser1)]; }
        }
        public static Image SampleUser2 {
            get { return Images.Images[nameof(SampleUser2)]; }
        }
        public static Image SampleUser3 {
            get { return Images.Images[nameof(SampleUser3)]; }
        }
        // API
        readonly static ConcurrentDictionary<long, Image> avatars = new ConcurrentDictionary<long, Image>();
        public static Image CreateAvatar(long id, Image image) {
            return avatars.GetOrAdd(id, x => ClipImage(image));
        }
        static Image ClipImage(Image image, int size = 128) {
            var avatar = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using(var g = Graphics.FromImage(avatar)) {
                g.Clear(Color.White);
                int clipSize = Math.Min(image.Width, image.Height);
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawImage(image, new RectangleF(0, 0, size, size), new RectangleF(0, 0, clipSize, clipSize), GraphicsUnit.Pixel);
            }
            return avatar;
        }
    }
}
