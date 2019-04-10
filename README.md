# WGET
---
### GNU WGET is a free utility for non-interactive download of files from the Web. It supports HTTP, HTTPS, and FTP protocols. This Application is a simplified version of WGET. Given a URL, application attempts to download that file using the HTTP 1.0 over TCP/IP and save it in the local disk. This file might be an HTML file, a binary image, a zip file,...,etc.
---

## Program Flow
**1.** Application (Client) opens TCP connection to server on port 80.<br/>
**2.** Application (Client) sends a request to the server.<br/>
**3.** Server sends response.<br/>
**4.** Server closes TCP connection.

---

## Application Only Deals with The Below Responses

* **"200 OK" :** Request succeeded! Saving this file to disk with the requested file name.

* **"301 Moved Permanently" :** The file is no longer at the URL you provided and the server is telling application the new URL where the file now permanently lives.

* **"400 Bad Request" :** This indicates an error in the application.

* **"404 Not Found" :** The file you requested was not found.
---

## Sample Testing Links

* <http://www.google.com/images/logos/google_logo_41.png> --> It will download an image

* <http://www.google.com/about/> --> It will download an HTML file

* <http://www.pdf995.com/samples/pdf.pdf> --> It will download a PDF file
---

## Sample Run of Application

![Sample Run](https://raw.githubusercontent.com/AbraamSameh/WGET/master/Images/Sample_Run_1.PNG "Sample Run Image")

![Sample Run](https://raw.githubusercontent.com/AbraamSameh/WGET/master/Images/Sample_Run_2.PNG "Sample Run Image")

---