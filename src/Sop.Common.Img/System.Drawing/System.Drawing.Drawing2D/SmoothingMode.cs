//
// System.DrawingCore.Drawing2D.SmoothingMode.cs
//
// Author:
//   Stefan Maierhofer <sm@cg.tuwien.ac.at>
//   Dennis Hayes (dennish@Raytek.com)
//
// (C) 2002/3 Ximian, Inc
// Copyright (C) 2004,2006 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

namespace System.DrawingCore.Drawing2D {
    /// <summary>
    /// 指定是否将平滑处理（消除锯齿）应用于直线、曲线和已填充区域的边缘。
    /// </summary>
	public enum SmoothingMode {
        /// <summary>
        /// AntiAlias      指定消除锯齿的呈现。  
        /// </summary>
		AntiAlias = 4,
        /// <summary>
        /// Default        指定不消除锯齿。  
        /// </summary>
		Default = 0,
        /// <summary>
        /// HighQuality  指定高质量、低速度呈现。  
        /// </summary>
		HighQuality = 2,
        /// <summary>
        /// HighSpeed   指定高速度、低质量呈现。  
        /// </summary>
		HighSpeed = 1,
        /// <summary>
        /// Invalid        指定一个无效模式。
        /// </summary>
		Invalid = -1,
        /// <summary>
        ///  None          指定不消除锯齿。 
        /// </summary>
		None = 3
 
    }
}
