# Malware Analysis Report: "BackupWin" Trojan

## 1. Introduction & Discovery Story
I was searching for `chromesetup` on Google and clicked the top Sponsored link. The website looked almost identical to the official Chrome download page. However, the URL was a Google Scripts subdomain:

- [Malicious URL (Google Scripts)](https://script.google.com/macros/s/AKfycbyFo0RnixkbDzmvVLqH9PuEotSmYh3wvyJMTAyzGsa8ki-asIK1caQhutUT4fs4EKY/exec?af_r=...)

From there, I clicked a `Download Chrome` button, which downloaded an EXE called `Chrome-64.exe` from:

- [chrome.homepagel.com](https://chrome.homepagel.com/) *(Warning: downloads Trojan immediately, use secure VM!)*

At this point, I noticed red flags:
- Why wasn’t it downloading from google.com?
- Why was Google sponsoring such a suspicious link?

I decided not to run it and uploaded the file to VirusTotal, followed by behavioral analysis in Kali Linux and Windows 10 VMs. The analysis revealed a trojan named **BackupWin**.

## 2. File Information Comparison

| Attribute            | Original Chrome Setup                         | Suspected Chrome Setup                       |
|----------------------|-----------------------------------------------|----------------------------------------------|
| **File Name**        | ChromeSetup.exe                               | Chrome-x64.exe                               |
| **MD5 Hash**         | `120b91990785664528f3b8829570e055`            | `528607dc316d1c0cd45ac8b72f97068b`           |
| **SHA-1 Hash**       | `c5a44fdde7bafe53c4c2893b015a8ca3840af219`    | `72c64c02a31736029713270f1dc8f24c0f576d1b`   |
| **SHA-256 Hash**     | `1e9e2cce86974afadfba1818195ab669f53b5bf46165c9b27da1a4c7e83198d7` | `2ad37c2bc96ab025b2b72601d16ce90904a5afffb3cc37e706308a4700da82d7` |
| **File Size**        | 11MB                                          | 130MB                                        |
| **File Type**        | PE32 executable                               | PE32 executable                              |
| **Original File Name** | UpdaterSetup.exe                              | AppApi.dll                                   |
| **Signature**        | Google LLC                                    | LLC Spectr                                   |
| **Company Name**     | Google LLC                                    | AppApi                                       |
| **Product Name**     | Installer                                     | AppApi                                       |
| **File Version**     | 136.0.7079.0                                  | 1.0.0.0                                      |

## 3. VirusTotal Scan Findings
- No antivirus flagged the file as malicious.
- Suspicious connections to `solarcellled.com` observed.
- Malicious C# script found at [solarcellled.com/api.php](https://solarcellled.com/api.php).

## 4. Suspicious Behavior Observed
The C# script performs:
- **Privilege Escalation**: Relaunches itself as administrator.
- **Windows Defender Exclusion**:
  ```powershell
  Add-MpPreference -ExclusionPath '%APPDATA%', '%TEMP%'
  ```
- **Encrypted Payload Download**:
  - Downloads AES-encrypted payload from:
  ```
  https://solarcellled.com/test.php?uuid=...
  ```
  - Decrypts and saves as `%APPDATA%\BackupWin\decrypted.exe`, runs silently.
- **Persistence via Scheduled Task**:
  - Name: `BackupWinTask`
  - Action: Executes malware at login with elevated privileges.

## 5. Dynamic Analysis Summary
### Key Findings
- Creates folders in hidden directories.
- Modifies numerous registry entries.
- Connects to internet, downloads malicious scripts.
- Installs persistence mechanisms.

### Tools Used
- Wireshark, ProcMon, ProcExp, Regshot, TCPView, FakeNet-NG, Strings

### Suspicious IP Addresses
- `69.62.119.2` (Germany, Hostinger) - solarcellled.com, srv760637.hstgr.cloud
- `69.62.119.17` (Germany, Hostinger) - blgzd.com

### Malicious Domains
- srv760637.hstgr.cloud
- blgzd.com
- solarcellled.com
- chrome.homepagel.com

### Created Directories
```
C:\Users\Malware\AppData\Local\Temp\.net\Chrome-x64\AV9JX02HGBnwvgGSvHtJPrgR6B2GBbA=
C:\Users\Malware\AppData\Local\Temp\_MEI63322
```

## Registry Changes
[View cleaned registry changes](https://pastebin.com/Y1f8R9np).

### Significant Registry Changes
- **Driver Installation** (`WinDivert1.1`):
  - Loaded into kernel; enables MITM attacks, packet manipulation.

- **Group Policy Manipulation**:
  - Added/deleted unknown GPO instances.

- **Network Configuration Manipulation**:
  - Removed DHCP IP/DNS settings to evade detection.

- **New Network Profiles**:
  - Registered spoofed or virtual networks.

- **File Extension Hijacking** (`.com`):
  - Hijacked `.com` extensions, redirected to malicious executable (`LaunchWinApp.exe`).

## Suspicious DLL Files
- **AppApi.dll**
  - SHA256: `2ad37c2bc96ab025b2b72601d16ce90904a5afffb3cc37e706308a4700da82d7`
  - [Decompiled source (via dnSpy)](https://pastebin.com/60P8uHGb)

## Attachments
- [Wireshark PCAP](#)
- [api.php Source Code](#)
- [AppApi.dll Source Code](#)
- [Registry Changes (Clean)](#)
- [Registry Changes (Full)](#)
- [VirusTotal Report](#)

## 6. Conclusion & Lessons Learned
- Trojan designed to evade antivirus detection, escalate privileges, persist, and deploy further payloads.

### Key Lessons
- AV misses do not imply safety.
- Always verify software sources.
- Behavioral analysis reveals truths signatures might miss.

## 7. Additional Notes
- Attacker frequently changes malicious download URLs to evade takedown.
- URL [solarcellled.com/test.php](https://solarcellled.com/test.php) appears disabled.

---

**Share this report to raise awareness and help others avoid similar malware threats!**
