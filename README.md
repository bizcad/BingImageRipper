# BingImageRipper
This simple application scrapes the daily background image from the Bing homepage.

It is for demonstrating the new(ish) HttpClient by reading the content of https://www.bing.com.

This is the first version and does not even have a version.  As a starting point it uses a Winforms app because it has System.Windows.Forms by default in the template from VS2022.  For development, I used the clipboard to capture the page contents which I could then look at in Notepad++.  By looking at the DevTools in Edge and I found that the captured page has two elements of interest.
- the image href which starts with `th?` and contains `1080`.
- the meta property *og.title* which contains a title for the image.

It uses simple string search IndexOf to locate the `th?` and `jpg` to substring the href.  It does a similar thing to substring the title.  From these, the program builds a file path to an images folder on my hard drive.  This path is hard coded YMMV.

It uses the HttpClient to read the bytes from the image url and saves the bytes to the file.

I am using the images for testing another application I am writing to get a web palette from an image similar to the excellent utiltity which may be found at 

<a href="https://icolorpalette.com/color-palette-from-images"> <img class="logo ezlazyloaded" src="https://icolorpalette.com/wp-content/themes/icolorpalette-child/logo.png?ezimgfmt=rs:230x36/rscb7/ngcb6/notWebP" alt="iColorpalette" style="max-width:230px" ezimgfmt="rs rscb7 src ng ngcb6" data-ezsrc="https://icolorpalette.com/wp-content/themes/icolorpalette-child/logo.png?ezimgfmt=rs:230x36/rscb7/ngcb6/notWebP" height="31" width="200" ezoid="0.5831869640450245"></a>

## Warning ##

**You should be aware that many of the images on Bing's home page are copywrited and you cannot use them publicly without getting rights from the owner.**
