using KooKoo.WebService.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KooKoo.WebService.Controllers {

    public class UploadController : ApiController {

        private readonly IStoryRepository m_storyRepo;

        public UploadController() {

            //TODO: injection
            m_storyRepo = new StoryRepository();
        }

        public async Task<HttpResponseMessage> PostMultipartStream([FromUri]Guid id) {

            if (!Request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(provider);

            if (provider.Contents.Any()) {
                Stream stream = provider.Contents.First().ReadAsStreamAsync().Result;
                if (stream.CanSeek) {
                    stream.Position = 0;
                }
                // TODO: resize and save as JPEg regardless. maybe allow more for height
                Image img = Image.FromStream(stream);
                if( img.Width > 1200 ) {
                    float ratio = (float)img.Width / (float)img.Height;

                    img = ResizeImage(img, new Size(1200, img.Height));
                } else if (img.Height > 1200) {
                    float ratio = (float)img.Width / (float)img.Height;

                    img = ResizeImage(img, new Size(img.Width, 1200));
                }
                
                Stream m = new MemoryStream();
                img.Save(m, ImageFormat.Jpeg);
                if (m.CanSeek) {
                    m.Position = 0;
                }
                m_storyRepo.UploadImage(id, m);
            }

            return Request.CreateResponse(HttpStatusCode.OK);            
        }

        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true) {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio) {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            } else {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage)) {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

    }
}
