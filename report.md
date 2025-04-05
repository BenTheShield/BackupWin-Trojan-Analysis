> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image1.png){width="5.002777777777778in"
> height="3.3361111111111112in"}
>
> **[Malware Analysis Report: "BackupWin" Trojan]{.underline}**
>
> 1\. Introduction & Discovery Story
>
> I was searching for **"chromesetup"** on Google and clicked the top
> **Sponsored** link. The website looked almost idencal to the official
> Chrome download page. However, the URL was a Google Scripts subdomain:
>
> From there, I clicked a **"Download Chrome"** buton, which gave me an
> EXE called Chrome-64.exe from:
>
> (**Warning!**Clicking this link will immediately download the EXE
> Trojan to your PC, Download it only in secure VM.)
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image2.png){width="6.725in"
> height="3.5277777777777777in"}
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image3.png){width="6.376388888888889in"
> height="3.0236111111111112in"}
>
> At this point, something felt off:\
> • Why wasn't it downloading from google.com?
>
> • Why was Google *sponsoring* a link like this?
>
> Instead of installing it, I uploaded the file to **VirusTotal** and
> ran behavior analysis in a **Kali Linux VM** & **Windows 10 VM** with
> Analysis Tools.
>
> The results revealed a trojan I'm calling **"BackupWin"** (named aer
> strings and file paths found in the code).
>
> 2\. Suspected File Info vs Original
>
> Chrome Setup Info

+-----------------------+-----------------------+-----------------------+
|                       | > **Original Chrome   | > **Suspected Chrome  |
|                       | > Setup**             | > Setup**             |
+=======================+=======================+=======================+
| > **File**\           | > **ChromeSetup.exe** | > **Chrome-x64.exe**  |
| > **Name**            |                       |                       |
+-----------------------+-----------------------+-----------------------+
| > **MD5 Hash**        | > 120b9199078         | > 528607dc316         |
|                       | 5664528f3b8829570e055 | d1c0cd45ac8b72f97068b |
+-----------------------+-----------------------+-----------------------+
| > **SHA-1 Hash**      | c5a44fdde7bafe53c4c   | > 72c64c02a3173602971 |
|                       | 2893b015a8ca3840af219 | 3270f1dc8f24c0f576d1b |
+-----------------------+-----------------------+-----------------------+
| > **SHA-**\           | > 1e9e2cce86974afad   | > 2ad37c2bc96ab025b2b |
| > **256**\            | a1818195ab669f53b5bf4 | 72601d16ce90904a5afff |
| > **Hash**            | > 616                 | > b3c                 |
|                       | 5c9b27da1a4c7e83198d7 | c37e706308a4700da82d7 |
+-----------------------+-----------------------+-----------------------+
| **File**\             | > **11MB**            | > **130MB**           |
| **Size**              |                       |                       |
+-----------------------+-----------------------+-----------------------+
| > **File**\           | > **PE32 executable   | > **PE32 executable   |
| > **Type**            | > (GUI) Intel 80386,  | > (GUI) Intel 80386,  |
|                       | > for MS Windows**    | > for MS Windows**    |
+-----------------------+-----------------------+-----------------------+
| **File Metadata       |                       |                       |
| (Exiool)**            |                       |                       |
+-----------------------+-----------------------+-----------------------+
| **Origin**\           | >                     | > **AppApi.dll**      |
| **al File Name**      |  **UpdaterSetup.exe** |                       |
+-----------------------+-----------------------+-----------------------+
| > **Signat ure**      | > **Google LLC**      | > **LLC Spectr**      |
|                       | >                     |                       |
|                       | > ![](ver             | ![](ver               |
|                       | topal_60080144495f4ef | topal_60080144495f4ef |
|                       | 8a9f1adb38cd45dc3/med | 8a9f1adb38cd45dc3/med |
|                       | ia/image4.png){width= | ia/image4.png){width= |
|                       | "6.269444444444445in" | "6.269444444444445in" |
|                       | > height="            | height="              |
|                       | 1.323611111111111in"} | 1.323611111111111in"} |
+-----------------------+-----------------------+-----------------------+
| > **Compa ny**\       | > **Google LLC**      | > **AppApi**          |
| > **Name**            |                       |                       |
+-----------------------+-----------------------+-----------------------+
| > **Produc t**\       | > **Installer**       | > **AppApi**          |
| > **Name**            |                       |                       |
+-----------------------+-----------------------+-----------------------+
| > **File**\           | > **Google            | > **AppApi**          |
| > **Descri pon**      | > Installer**         |                       |
+-----------------------+-----------------------+-----------------------+
| > **File**\           | > **136.0.7079.0**    | > **1.0.0.0**         |
| > **Versio n**        |                       |                       |
+-----------------------+-----------------------+-----------------------+

> 3\. VirusTotal Scan
>
> **Here's what I discovered from the VirusTotal scan:**
>
> • **No anvirus engines flagged it as malicious (possibly a new or
> obfuscated Trojan).**
>
> • However, in the **Behavior** tab, I noced suspicious connecons to
> **solarcellled.com** and other
>
> domains.
>
> • I visited **htps://solarcellled.com/api.php** and found a C# script
> with clear malware behavior.
>
> • **Please refer to the atachments secon below to view the full
> VirusTotal report.**
>
> 4\. Suspicious Behavior Observed
>
> I reviewed the **C# scriptfrom api.php**. In case it's removed, I've
> backed it up, (please look on the
>
> atachments below):
>
> **The script performs the following:**
>
> **1. Privilege Escalaon Atempt**
>
> • Tries to **re-launch itself as administrator** using runas.
>
> **2. Windows Defender Exclusion**
>
> • Executes PowerShell:

+-----------------------------------------------------------------------+
| > Add-MpPreference -ExclusionPath \'%APPDATA%\', \'%TEMP%\'           |
+=======================================================================+
+-----------------------------------------------------------------------+

> **\[!\]** Tells Defender to ignore those folders, where malware
> usually hides.
>
> **3. Encrypted Payload Download**
>
> • Downloads an AES-encrypted payload from:
>
> https://solarcellled.com/test.php?uuid=\...
>
> • Decrypts it and saves it as:
>
> %APPDATA%\\BackupWin\\decrypted.exe
>
> • Then runs it silently.
>
> **4. Persistence via Scheduled Task**
>
> Creates a Windows Scheduled Task:

+-----------------------+-----------------------+-----------------------+
| 5\.                   | •                     | > Name: BackupWinTask |
+=======================+=======================+=======================+
|                       | •                     | > Acon: Run the       |
|                       |                       | > malware at every    |
|                       |                       | > login with          |
|                       |                       | > **highest           |
|                       |                       | > privileges**.       |
+-----------------------+-----------------------+-----------------------+
|                       |                       | > Dynamic Analysis    |
+-----------------------+-----------------------+-----------------------+

> **[Main Details]{.underline}**
>
> \- The Trojan creates folders in hidden directories.
>
> \- The Trojan modifies numerous registry entries.
>
> \- The Trojan connects to the internet and downloads malicious
> scripts.
>
> \- Overall, The Trojan Downloads, decrypts, and executes a secondary
> payload; disables
>
> Defender; installs a persistence mechanism; hides network acvity.
>
> **[Tools Utilized:]{.underline}**
>
> Wireshark, Process Monitor (procmon), Process Explorer (procexp),
> Regshot, TCPView, FakeNet-NG,
>
> Strings.
>
> **[Suspicous IP Address:]{.underline}**\
> 69.62.119.2 (Germany, Hosted by Hosnger) -\> +\
> \
> 69.62.119.17 (Germany, Hosted by Hosnger) -\>\
> **[Malicious Domains:]{.underline}**
>
> **[Directories Created:]{.underline}**\
> C:\\Users\\Malware\\AppData\\Local\\Temp\\.net\\Chrome-x64\\AV9JX02HGBnwvgGSvHtJPrgR6B2GBbA=
> C:\\Users\\Malware\\AppData\\Local\\Temp\\\_MEI63322
>
> **[Registry Analysis (via Regshot)]{.underline}**
>
> Using Regshot, I captured a snapshot of the registry **before and aer
> running the trojan**.
>
> Cleaned results are available here: (If you require the full output,
> please refer to the atachments below.)
>
> This program:
>
> 1\. [Driver Installaon: \"**WinDivert1.1**\"]{.underline}\
> o **Registry**:
> HKLM\\SYSTEM\\CurrentControlSet\\Services\\WinDivert1.1 o **File
> Path:**\
> C:\\Users\\Malware\\AppData\\Local\\Temp\\\_MEI49\~1\\WinDivert64.sys
> This driver was loaded into the **[kernel]{.underline}**, making it
> parcularly dangerous.
>
> **WinDivert** is commonly used for [network packet manipulaon,
> enabling traffic redireco]{.underline}n, [sniffing, or
> Man-in-the-Middle (MITM) atac]{.underline}ks.
>
> (See supporng screenshots below that shows that it\'s actually running
> on the kernel.)
>
> 2\. [Group Policy Manipulaon]{.underline}\
> o **Registry**:
> HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Group
> Policy\\ServiceInstances\\{GUID}\
> o **Observaon**:\
> Unknown GPO service instances were added and deleted --- likely to
> manipulate system policies or bypass restricons.
>
> 3\. [Network Configuraon Manipulao]{.underline}n\
> o **Registry**:\
> HKLM\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfa
> ces\\{GUID}\
> o **Observaon**:\
> The trojan removed DHCP-based IP/DNS sengs, possibly to prevent
> conflict or detecon.
>
> Combined with the WinDivert driver, this suggests **low-level control
> over network traffic**.
>
> 4\. [New Network Profiles/Signature]{.underline}s\
> o **Registry**: HKLM\\SOFTWARE\\Microsoft\\Windows\
> NT\\CurrentVersion\\NetworkList\\Profiles\\{GUID}\
> o **Registry**: HKLM\\SOFTWARE\\Microsoft\\Windows\
> NT\\CurrentVersion\\NetworkList\\Signatures\\Unmanaged\\{GUID} o
> **Observaon:**\
> Mulple new **network profiles** were registered --- potenally spoofed
> or virtual networks created by the trojan to reroute traffic
>
> 5\. [Suspicious File Extension Hijacking]{.underline}\
> o **Registry**:\
> HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExt
> s\\.com/redirect\
> o **Observaon**:\
> Malicious hijack of the .com executable file extension was detected.
>
> It redirected .com files to open with LaunchWinApp.exe, possibly
> execung malicious code instead of legimate programs.
>
> **[Suspicious DLL Files Created]{.underline}**
>
> The core logic DLL of the malware is:
>
> • **Filename:** AppApi.dll
>
> • **SHA256:**
> 2ad37c2bc96ab025b2b72601d16ce90904a5aff3cc37e706308a4700da82d7
>
> • **Decompiled Source:***(via dnSpy)*
>
> This program:
>
> • Downloads malicious code from the internet (api.php) • **Executes it
> inmemory** using the C# scripng engine (Roslyn)
>
> The malware generated **over 60 addional DLLs**, located in:
>
> • C:\\Users\\Malware\\AppData\\Local\\Temp\\.net\\Chrome-x64
>
> **[Attachments]{.underline}**
>
> **1.Wireshark traffic:**\
> **a.**GitHub**[:]{.underline}**\
> **2.api.php source code:**\
> **a.**Pastebin**:**\
> **b.**GitHub**:**\
> **3.AppApi.dll source code:**\
> **a.**Pastebin**:**\
> **b.**GitHub**:**\
> **4.Registry changes that the trojan made:**\
> **a.**Pastebin**[:]{.underline}**\
> **b.**GitHub (Cleaned up version)[:]{.underline}\
> **c.** GitHub (Full version):\
> **5.VirusTotal Scan Report**\
> **a.**VirusTotal:
>
> 6\. Conclusion & Lessons Learned
>
> Even though no anvirus flagged the file, the behavior and script logic
> clearly show malicious intent. This is a Trojan Downloader designed to
> bypass detecon, run with elevated privileges, and deliver more
> payloads.
>
> **Key Lessons:**\
> 1.No AV detecons doesn\'t mean it\'s Safe!
>
> 2.Always check Behavior tab + external domains\
> 3.Sponsored Ads Can Be Dangerous -- even on Google! 4.Verify official
> domains before downloading soware 5.Malware evolves -- atackers oen
> bypass signatures 6.Code review reveals truth, even when AV doesn't
>
> 7\. Additional Notes
>
> 1.The atacker frequently changes the URL within the fake Chrome
> download page, possibly to evade takedowns and blocking lists.
>
> 2\. The URL appears to be broken at present.
>
> 8\. Additional Screenshots
>
> **1.**Screenshot taken from the folder
>
> AppData\\Local\\Temp\\\_MEI63322

![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image5.png){width="7.319444444444445in"
height="4.894444444444445in"}

> **2.** Screenshot taken from the folder\
> AppData\\Local\\Temp\\.net\\Chrome-\
> x64\\AV9JX02HGBnwvgGSvHtJPrgR6B2GBbA=
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image6.png){width="7.625in"
> height="7.1930555555555555in"}
>
> **3.** Screenshot taken from Process Monitor, you can see that it have
> a connecon to\
> **htp://srv760637.hstgr.cloud/**
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image7.png){width="9.129165573053369in"
> height="2.738888888888889in"}
>
> **4.** Screenshot taken from Process Monitor, you can see
>
> that it\'s created the main **AppApi.dll** in the folder
>
> AppData\\Local\\Temp\\.net\\Chrome-
>
> x64\\AV9JX02HGBnwvgGSvHtJPrgR6B2GBbA=
>
> **5.**Screenshot taken from the CMD, this can proof that the
> **Windervert1.1 driver installed and running on the kernel**
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image8.png){width="7.590277777777778in"
> height="4.254166666666666in"}
>
> **6.**Screenshot taken from ipinfo.io, this is the malicious IP
> address:
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image9.png){width="8.181944444444444in"
> height="6.761111111111111in"}
>
> **7.**Screenshot taken from VirusTotal at March 29, 2025:
>
> ![](vertopal_60080144495f4ef8a9f1adb38cd45dc3/media/image10.png){width="8.094443350831146in"
> height="5.61388779527559in"}
>
> **Share this report to raise awareness and**
>
> **help others avoid falling for similar traps**!
