using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using Serilog;

namespace Sop.Common.Img.Tests
{
    public class ImgSimilarTest
    {
        private IList<ImgSimilarInfo> imageFilePaths;
        private IList<ImgSimilarInfo> imgHashList;
        private string filePath;
        private Serilog.ILogger log;
        [SetUp]
        public void Setup()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            filePath = $"{path}Resources\\Similar";
            imageFilePaths = new List<ImgSimilarInfo>();
            for (int i = 1; i < 15; i++)
            {
                imageFilePaths.Add(new ImgSimilarInfo()
                {
                    Path = filePath + $"\\1 ({i}).jpg",
                    Name = $"1({i}).jpg"
                });
            }
            log = new LoggerConfiguration()
               .WriteTo.File("log.txt",
                   rollingInterval: RollingInterval.Day,
                   rollOnFileSizeLimit: true)
               .CreateLogger();


        }
        [Test]
        public void GetHash_Tests()
        {
            imgHashList = new List<ImgSimilarInfo>();
            foreach (var path in imageFilePaths)
            {
                var aa = new ImgSimilar(path.Path);
                aa.Width = 16;
                aa.Height = 16;
                var hash = aa.GetHash(); ;
                imgHashList.Add(new ImgSimilarInfo()
                {
                    Name = path.Name,
                    Path = path.Path,
                    Hash = hash

                });
                //log.Information(path.Name + "----" + hash);
                foreach (var t in imgHashList)
                {
                    var count = ImgSimilar.CalcSimilarDegree(hash, t.Hash);
                    if (path.Name != t.Name)
                    {
                        log.Information($"{path.Name}:{t.Name}---{count}");
                    }
                }
                log.Information("----");


            }
            log.Information("----");


            Assert.IsNotEmpty(imgHashList);
        }


    }
    public class ImgSimilarInfo
    {
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}