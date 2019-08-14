# Sop.Common.Img

# 背景

此项目是有https://github.com/Sopcce/.Net-Common-Utility中的一个小部分模块功能。此处拿出来单独维护
>**以C# Net(dotnet.core)仿照七牛云图片处理类实现的C#帮助类**

# 远景
> ### 做最全、最广、最细的图片处理基类

# 需要
- 没钱，用不起七牛云的图片处理，需要自己搭建服务器，自己存储图片。
- 没有图片识别，还是没钱，图片识别腾讯云做的最好，无奈 还是没钱需要使用Tesseract自己搭建一个图文识别功能。
- 
# 图片处理使用说明
## 介绍
 请求看七牛云
 https://developer.qiniu.com/dora/manual/3683/img-directions-for-use
 的图片处理功能
----
## 功能介绍：

----

| 接口  | 简介   |   
|:------: | :------------------------------  |
|**1** ImgLim      | 将JPEG、PNG格式的图片实时压缩而尽可能不影响画质。详细信息请参阅  [**图片瘦身 (ImgLim)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-thin-body-imageslim)。  |  
|**2** ImgView |可对图片进行缩略操作，生成各种缩略图，可以获取图片格式、大小、色彩模型信息。 详细信息请参阅 [**图片基本处理 (ImgView)**](https://github.com/csharphelp/Sop.Common.Img/wiki/basic-processing-images-imageview)。   |   
|**3** ImgManager|提供了一系列高级图片处理功能，缩放、裁剪、旋转等。详细信息请参阅 [**图片高级处理 (ImgManager)**](https://github.com/csharphelp/Sop.Common.Img/wiki/the-advanced-treatment-of-images-imagemogr)。   |   
|**4** ImgOcr        |  图片识别功能，可以识别图片的基本信息 详细信息请参阅 [**图片基本信息 (ImgOcr)**](https://github.com/csharphelp/Sop.Common.Img/wiki/pictures-basic-information-imageinfo)。 |  
|**5** ImgExif  | 获取数码相机照片的可交换图像文件格式。详细信息请参阅 [**图片 EXIF 信息(ImgExif)**]([exif](https://github.com/csharphelp/Sop.Common.Img/wiki/exif))。 |  
|**6** ImgWaterMark| 提供两种水印：图片水印、文字水印。关于 watermark 接口的详细信息请参阅 [**图片水印处理 (ImgWaterMark)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-watermarking-processing-watermark)。 |  
|**7** imageAve        |图片平均色调接口用于计算一幅图片的平均色调。关于 imageAve 接口的详细信息请参阅  [**图片主色调 (imageAve)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-average-hue-imageave)。 |  
|**8** animate        |    动图合成接口用于将数张图片合成 GIF。关于 animate 接口的详细信息请参阅 [**动图合成 (animate)**](https://github.com/csharphelp/Sop.Common.Img/wiki/animate)。 |  



## 完成度统计

| 接口  | 开发完成度   |   测试用例|
|:------: | :------------------------------:  |:------------------------------:  |
|**1**imageslim |  90%    |    50%   |   
|**2**imageView |   50%   |     50%   |   
|**3**imageMogr |   50%   |     50%   |   
|**4**imageInfo |   50%   |    50%   |   
|**5** exif     |   50%   |    50%   |   
|**6** watermark|  50%  |    50%   |   
|**7** imageAve |   50%    |    50%   |   
|**8** animate  |  50%  |    50%   |   

 

# Nuget

> Install-Package Sop.Common.Img -Version 1.2.2
####  请尽可能的使用最新版本

# OCR身份证识别


#问题反馈
目前，请反馈在 issues

#更新日志

+ 列表一
+ 2019.06
  + 1. 项目迁移。
  + 2. 修改文档。
+ 2019.05
    * 列表一
    * 列表二
    * 列表三
-  更新提交此项目


# License
[**GNU General Public License v3.0**](https://github.com/csharphelp/Sop.Common.Img/blob/master/LICENSE)
