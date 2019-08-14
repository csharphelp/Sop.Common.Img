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
> ### 请看 [七牛云的图片处理功能介绍文档了解本项目功能](https://developer.qiniu.com/dora/manual/3683/img-directions-for-use)
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
|**6** ImgWaterMark| 提供两种水印：图片水印、文字水印。关于 ImgWaterMark 接口的详细信息请参阅 [**图片水印处理 (ImgWaterMark)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-watermarking-processing-watermark)。 |  
|**7** ImgAve        |图片平均色调接口用于计算一幅图片的平均色调。关于 ImgAve 接口的详细信息请参阅  [**图片主色调 (ImgAve)**](https://github.com/csharphelp/Sop.Common.Img/wiki/image-average-hue-imageave)。 |  
|**8** ImgAnimate        |    动图合成接口用于将数张图片合成 GIF。关于 ImgAnimate 接口的详细信息请参阅 [**动图合成 (ImgAnimate)**](https://github.com/csharphelp/Sop.Common.Img/wiki/animate)。 |  



## 完成度统计

|**序号**|**接口**|**完成度**|**测试用例** | **备注**     |
|:--:  |:-----    | :---|:----  |-------  |
|**1** |ImgLim      |100%  |  99%   |基本完成 |   
|**2** |ImgView     |30%   |  50%   |待测试   |    
|**3** |ImgManager  |50%   |  50%   |待测试   |   
|**4** |ImgOcr      |5%    |  50%   |待测试   |   
|**5** |ImgExif     |80%   |  50%   |待测试   |   
|**6** |ImgWaterMark|10%   |  50%   |         |   
|**7** |ImageAve    |100%  |  99%   |基本完成 |   
|**8** |ImgAnimate  |100%  |  99%   |基本完成 |   

 

# Nuget

> Install-Package Sop.Common.Img -Version 1.2.3


## 请尽可能的使用最新版本,或者根据源码自己修改编译使用

# OCR身份证识别
 
 计划使用Tesseract 完成ocr 图片识别部分，只考虑图片，

# 问题反馈
目前，请反馈在 issues

# 更新日志


+ 2019.08
  + 1. 修改ImageAve，更新文档，计划完成水印部分。
  + 2. 修改文档。
  + 3. 修改ImgExif 提交代码，完成测试部分
+ 2019.06
  + 1. 项目迁移。
  + 2. 修改文档。
+ 2019.05
    * 修改ImgAnimate 动画部分，完成测试
    * 修改ImgAnimate 动画部分，完成测试，提交代码
-  更新提交此项目


# License
[**GNU General Public License v3.0**](https://github.com/csharphelp/Sop.Common.Img/blob/master/LICENSE)
