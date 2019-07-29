# Sop.Common.Img

# 背景

此项目是有https://github.com/Sopcce/.Net-Common-Utility中的一个小部分模块。此处拿出来单独维护
>**以C# Net(dotnet.core)仿照七牛云图片处理类实现的C#帮助类**

# 远景
> ### 做最全、最广、最细的图片处理基类

# 图片处理使用说明
## 介绍
### 图片处理服务为图片文件提供以下功能：
1. 对图片进行缩略操作
2. 图片缩放、裁剪、旋转
3. 获取图片格式、大小、色彩模型信息
4. 提供数码相机照片的可交换图像文件格式
5. 图片添加图片、文字、图文混合水印
6. 计算图片的平均色调
----
### 注意：
**同步处理 ** ：
- 同步处理只支持 20MB 以内的图片，超过 20MB 的图片需要使用异步处理
- 同步处理后的图片w和h参数不能超过9999像素，总像素不得超过24999999（2500w-1）像素
- 同步处理前的图片w和h参数不能超过3万像素，总像素不能超过1.5亿像素

**异步处理 **：
- 异步处理无大小限制
- 异步处理后的图片w和h参数不能超过14999像素，总像素不得超过59999999（6000w-1）像素
- 异步处理前的图片w和h参数不能超过3万像素，总像素不能超过1.5亿像素
- 同步处理和异步处理，处理后图片文件大小没有限制
----

| 接口  | 简介   |   
| :------: | :------------------------------  |
|#1 imageslim      | 图片瘦身（imageslim）将存储的JPEG、PNG格式的图片实时压缩而尽可能不影响画质。关于 图片瘦身（imageslim）接口的详细信息请参阅  [**图片瘦身 (imageslim)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-thin-body-imageslim)。  |  
|#2 imageView2 |  图片基本处理接口可对图片进行缩略操作，生成各种缩略图。imageView2 接口可支持处理的原图片格式有 psd、jpeg、png、gif、webp、tiff、bmp。 关于 imageView2 接口的详细信息请参阅[图片基本处理 (imageView2)](https://github.com/csharphelp/Sop.Common.Img/wiki/basic-processing-images-imageview2)。   |   
|#3 imageMogr2        |    图片高级处理接口为开发者提供了一系列高级图片处理功能，包括缩放、裁剪、旋转等。imageMogr2 接口可支持处理的原图片格式有 psd、jpeg、png、gif、webp、tiff、bmp。关于 imageMogr2 接口的详细信息请参阅 [**图片高级处理 (imageMogr2)**](https://github.com/csharphelp/Sop.Common.Img/wiki/the-advanced-treatment-of-images-imagemogr2)。   |   
|#4 imageInfo        |  图片基本信息接口可以获取图片格式、大小、色彩模型信息。在图片下载 URL 后附加 imageInfo 指示符（区分大小写），即可获取 JSON 格式的图片基本信息。关于 imageInfo 接口的详细信息请参阅 [**图片基本信息 (imageInfo)**](https://github.com/csharphelp/Sop.Common.Img/wiki/pictures-basic-information-imageinfo)。 |  
|#5 exif        |    图片 EXIF 信息接口通过在图片下载 URL 后附加 exif 指示符（区分大小写）获取数码相机照片的可交换图像文件格式。关于 exif 接口的详细信息请参阅图片EXIF信息  [**exif**]([exif](https://github.com/csharphelp/Sop.Common.Img/wiki/))。 |  
|#6 watermark        |   提供两种水印：图片水印、文字水印。关于 watermark 接口的详细信息请参阅 [**图片水印处理 (watermark)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-watermarking-processing-watermark)。 |  
|**7** imageAve        |图片平均色调接口用于计算一幅图片的平均色调。关于 imageAve 接口的详细信息请参阅  [**图片主色调 (imageAve)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-average-hue-imageave)。 |  
|**8** animate        |    动图合成接口用于将数张图片合成 GIF。关于 animate 接口的详细信息请参阅 [**动图合成 (animate)**](https://github.com/csharphelp/Sop.Common.Img/wiki/animate)。 |  
 

# Nuget

> Install-Package Sop.Common.Img -Version 1.2.1
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