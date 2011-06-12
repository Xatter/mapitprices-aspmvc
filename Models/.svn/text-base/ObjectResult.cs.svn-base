using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MapItPrices.Models
{
    public class ObjectResult : ActionResult
    {
        private static UTF8Encoding UTF8 = new UTF8Encoding(false);

        public object Data { get; set; }

        public Type[] IncludedTypes = new[] { typeof(object) };

        public ObjectResult(object data)
        {
            this.Data = data;
        }

        public ObjectResult(object data, Type[] extraTypes)
        {
            this.Data = data;
            this.IncludedTypes = extraTypes;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //if (context.HttpContext.Request.Headers["Content-Type"].Contains("application/json"))
            //{
                new JsonResult()
                {
                    Data = this.Data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }.ExecuteResult(context);
            //}
            //else
            //{
            //    using (MemoryStream stream = new MemoryStream(500))
            //    {
            //        using (var xmlWriter = XmlTextWriter.Create(stream,
            //            new XmlWriterSettings
            //            {
            //                OmitXmlDeclaration = true,
            //                Encoding = UTF8,
            //                Indent = true
            //            }))
            //        {
            //            new XmlSerializer(this.Data.GetType(), IncludedTypes)
            //            .Serialize(xmlWriter, this.Data);
            //        }

            //        new ContentResult
            //        {
            //            ContentType = "text/xml",
            //            Content = UTF8.GetString(stream.ToArray()),
            //            ContentEncoding = UTF8
            //        }
            //        .ExecuteResult(context);
            //    }
            //}
        }
    }
}