# BingImageRipper
This simple application scrapes the daily background image from the Bing homepage.

It is for demonstrating the new(ish) HttpClient by reading the content of https://www.bing.com.

This is the first version and does not even have a version.  As a starting point it uses a Winforms app because it has System.Windows.Forms by default in the template from VS2022.  For development, I used the clipboard to capture the page contents which I could then look at in Notepad++.  By looking at the DevTools in Edge and I found that the captured page has two elements of interest.
- the image href which starts with `th?` and contains `1080`.
- the meta property *og.title* which contains a title for the image.


