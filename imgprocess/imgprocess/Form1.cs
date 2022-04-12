using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using AForge.Imaging.Filters;
using System.Drawing.Drawing2D;
using AForge.Imaging;


namespace imgprocess
{
    public partial class Form1 : Form
    {
        Bitmap aa;
        Boolean opened = true;
        System.Drawing.Image file;
        Graphics graphics;
        Boolean curoermoving = false;
        Pen curoerpen;
        Point PX, PY;
        Bitmap bm,orgimg,resimg;
        Boolean peen = false;
        int x, y, sX, sY, cX, cY;
        int index;
        
        
        


        public Form1()

        {
            
            InitializeComponent();
            // bm = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            bm = new Bitmap(resizeImage(pictureBox1.Image, new Size(950, 605)));
            graphics = Graphics.FromImage(bm);
            curoerpen = new Pen(Color.Black, 7);
            pictureBox1.Image = bm;
            resimg = new Bitmap(pictureBox1.Image);
            index = 0;

            // pictureBox1.Invalidate();

            /*         
             *         
             *         InitializeComponent();
            bm = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            graphics = Graphics.FromImage(bm);
            curoerpen = new Pen(Color.Black, 7);
            pictureBox1.Image = bm;




            bm = new Bitmap(pictureBox1.Image);
            graphics = pictureBox1.CreateGraphics();
            curoerpen = new Pen(Color.Black, 7);
            pictureBox1.Image = bm;
            */
        }
        void saveImage()
        {
   
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName);

                    format = ImageFormat.Png;


                /*  Bitmap bmp = bm;
                  Rectangle rec = new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height);
                  pictureBox1.DrawToBitmap(bmp, rec);
                  pictureBox1.Image = bm;
                  pictureBox1.Image.Save(sfd.FileName, format);

                 
                   Bitmap btm = bm.Clone(new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height), bm.PixelFormat);
                   btm.Save(sfd.FileName, format);*/
                bm.Save(sfd.FileName, format);

            }
        }
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private void load_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = resizeImage(pictureBox1.Image, new Size(950, 605));
            resimg = new Bitmap(resizeImage(pictureBox1.Image, new Size(950, 605)));
            bm = new  Bitmap(pictureBox1.Image);
            orgimg = new Bitmap(pictureBox1.Image);

            opened = true;

        }

        void reload()
        {
            if (!opened)
            {
                MessageBox.Show("Error: Please open an image first");
            }

            else
            {

                pictureBox1.Image = resizeImage(orgimg, new Size(950, 605));
                bm = orgimg;
                file = pictureBox1.Image;
                opened = true;
            }
        }
        void sepia1()
        {

            System.Drawing.Bitmap image = (Bitmap)pictureBox1.Image;
            AForge.Imaging.Filters.Sepia filter = new AForge.Imaging.Filters.Sepia();
            pictureBox1.Image = filter.Apply(image);
            //RChanges.Clear();
        }
        void invert()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {

                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{-1, 0, 0, 0, 1.00f},
                    new float[]{0, -1, 0, 0, 1.00f},
                    new float[]{0, 0, -1, 0, 1.00f},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{1, 1, 1, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;

            }
        }
        void sepia()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {

                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{0.393f, 0.349f, 0.272f, 0, 0},
                    new float[]{0.769f, 0.686f, 0.534f, 0, 0},
                    new float[]{.189f, .168f, .131f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;

            }
        }
        void grayscale()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {

                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{0.33f, 0.33f, 0.33f, 0, 0},
                    new float[]{0.59f, 0.59f, 0.59f, 0, 0},
                    new float[]{0.11f, 0.11f, 0.11f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;

            }
        }
        void blackAndWhite()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {

                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1.5f, 1.5f, 1.5f, 0, 0},
                    new float[]{1.5f, 1.5f, 1.5f, 0, 0},
                    new float[]{1.5f, 1.5f, 1.5f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{-1, -1, -1, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;

            }
        }
        void polaroid()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {

                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1.438f, -0.062f, -0.062f, 0, 0},
                    new float[]{-0.122f, 1.378f, -0.122f, 0, 0},
                    new float[]{-0.016f, -0.016f, 1.483f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{-0.03f, 0.05f, -0.02f, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;

            }
        }
        void GaussianSharp()
        {

            System.Drawing.Bitmap image = (Bitmap)pictureBox1.Image;
            AForge.Imaging.Filters.GaussianSharpen filter = new AForge.Imaging.Filters.GaussianSharpen();
            pictureBox1.Image = filter.Apply(image);
            

        }

        void brightness()
        {
            float changeb = trackBar1.Value * 0.1f;
            float changec = trackBar2.Value * 0.1f;
            float changes = trackBar3.Value * 0.1f;
            // float changealpha = trackBar3.Value * 0.1f;
            // float changep = trackBar3.Value * 0.1f;

            trackBar1.Text = changeb.ToString();
            trackBar2.Text = changec.ToString();
            trackBar3.Text = changes.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {



                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1, 0, 0, 0, 0},
                    new float[]{0, 1, 0, 0, 0},
                    new float[]{0, 0, 1, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0+changeb, 0+changeb, 0+changeb, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;


            }
        }
        void contrast()
        {
            float changeb = trackBar1.Value * 0.1f;
            float changec = trackBar2.Value * 0.1f;
            float changes = trackBar3.Value * 0.1f;
            float t = 0;
            // float changealpha = trackBar3.Value * 0.1f;
            // float changep = trackBar3.Value * 0.1f;

            trackBar1.Text = changeb.ToString();
            trackBar2.Text = changec.ToString();
            trackBar3.Text = changes.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {



                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{1+changec, 0, 0, 0, 0},
                    new float[]{0, 1+changec, 0, 0, 0},
                    new float[]{0, 0, 1+changec, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{t, t, t, 0, 1}
                });

                if (changec == 0)
                    t = (1f - changec) / 2f;

                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;


            }
        }

        void saturation()
        {
            float changeb = trackBar1.Value * 0.1f;
            float changec = trackBar2.Value * 0.1f;
            float changes = trackBar3.Value * 0.1f;
            // float changealpha = trackBar3.Value * 0.1f;
            // float changep = trackBar3.Value * 0.1f;

            float lumR = 0.3086f;

            float lumG = 0.6094f;

            float lumB = 0.0820f;
            float sr = (1 - changes) * lumR;

            float sg = (1 - changes) * lumG;

            float sb = (1 - changes) * lumB;



            trackBar1.Text = changeb.ToString();
            trackBar2.Text = changec.ToString();
            trackBar3.Text = changes.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {



                System.Drawing.Image img = pictureBox1.Image;                             // storing image into img variable of image type from picturebox1
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   /* creating a bitmap of the height of imported picture in picturebox which consists of the pixel data for a graphics image
                                                                        and its attributes. A Bitmap is an object used to work with images defined by pixel data.*/

                ImageAttributes ia = new ImageAttributes();                 //creating an object of imageattribute ia to change the attribute of images
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       // now creating the color matrix object to change the colors or apply  image filter on image
                {
                    new float[]{sr+changes, sr, sr, 0, 0},
                    new float[]{sg, sg+changes, sg, 0, 0},
                    new float[]{sb, sb, sb+changes, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           //pass the color matrix to imageattribute object ia
                Graphics g = Graphics.FromImage(bmpInverted);   /*create a new object of graphics named g, ; Create graphics object for alteration.
                                                            Graphics newGraphics = Graphics.FromImage(imageFile); is the format of loading image into graphics for alteration*/

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                /*   g.drawimage(image, new rectangle(location of rectangle axix-x, location axis-y, width of rectangle, height of rectangle),
               location of image in rectangle x-axis, location of image in rectangle y-axis, width of image, height of image,
               format of graphics unit,provide the image attributes   */


                g.Dispose();                            //Releases all resources used by this Graphics.
                pictureBox1.Image = bmpInverted;


            }
        }
        private void save_Click(object sender, EventArgs e)
        {
            bm = new Bitmap(resizeImage(pictureBox1.Image, new Size(950, 605)));
            graphics = Graphics.FromImage(bm);
            pictureBox1.Image = bm;
            orgimg = bm;
            saveImage();
        }

        private void onfilter_Click(object sender, EventArgs e)
        {
            reload();
            sepia1();
        }

        private void invert_Click(object sender, EventArgs e)
        {
            reload();
            invert();
        }

        private void wood_Click(object sender, EventArgs e)
        {
            reload();
            sepia();
        }

        private void gray_Click(object sender, EventArgs e)
        {
            reload();
            grayscale();
        }

        private void bandw_Click(object sender, EventArgs e)
        {
            reload();
            blackAndWhite();
        }

        private void polaroidbtn_Click(object sender, EventArgs e)
        {
            reload();
            polaroid();
        }

        private void sharp_Click(object sender, EventArgs e)
        {
            reload();
            GaussianSharp();
        }

        private void orginal_Click(object sender, EventArgs e)
        {
            
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            reload();
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            brightness();
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            contrast();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            saturation();
        }

        private void blackbox_Click(object sender, EventArgs e)
        {
            PictureBox color = (PictureBox)sender;
            curoerpen.Color = color.BackColor;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            curoerpen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            curoerpen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            curoermoving = true;
           
            PY = e.Location;
            cX = e.X;
            cY = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            curoermoving = false;
            sX = x - cX;
            sY = y - cY;
            if (index == 2)
            {
                graphics.DrawEllipse(curoerpen, cX, cY, sX, sY);
            }
            if (index == 3)
            {
                graphics.DrawRectangle(curoerpen, cX, cY, sX, sY);
            }
            if (index == 4)
            {
                graphics.DrawLine(curoerpen, cX, cY, x, y);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (peen) { 
            if(curoermoving)
            {
                    if (index == 1) { 
                PX = e.Location;
                graphics.DrawLine(curoerpen, PX,PY);
            
                PY = PX;
                    }
                }
                 
            pictureBox1.Refresh();
                x = e.X;
                y = e.Y;
                    sX = e.X - cX;
                    sY = e.Y - cY;
                
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void rectangel_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void lineb_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void filters_Click(object sender, EventArgs e)
        {
            panea.Visible = false;
            panel2.Visible = true;
            orgimg = new Bitmap(pictureBox1.Image);
            bm =orgimg;


        }

        private void pen_Click(object sender, EventArgs e)
        {
            peen = true;
            panea.Visible = true;
            panel2.Visible = false;
            bm = new Bitmap(resizeImage(pictureBox1.Image, new Size(950, 605)));
            graphics = Graphics.FromImage(bm);
            pictureBox1.Image = bm;
            orgimg = bm;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnpen_Click(object sender, EventArgs e)
        {
            curoerpen = new Pen(Color.Black, 2);
            index = 1;
        }

        private void brushbtn_Click(object sender, EventArgs e)
        {
            curoerpen = new Pen(Color.Black, 10);
            index = 1;
        }

        private void rest_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = resimg;
            orgimg = new Bitmap(pictureBox1.Image);
            bm = new Bitmap(pictureBox1.Image);
            panea.Visible = false;
            panel2.Visible = false;

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }
    }
}
