# 🛡️ BackupWin Trojan – Malware Analysis Report

This repository documents a detailed analysis of the "BackupWin" trojan, initially disguised as a Google Chrome installer. It includes behavior analysis, payload details, registry/network modifications, and defense mechanisms observed.

---

## 📌 Overview

- **Name:** BackupWin Trojan
- **Type:** Downloader / Persistent Malware
- **Initial Vector:** Fake Chrome installer via Google Sponsored Ad
- **Analysis Date:** April 2025
- **Researcher:** BenTheShield

---

## 🔗 Initial Malicious URLs

- Fake Chrome installer: `https://chrome.homepagel.com/Chrome-x64.exe`
- Google Ads redirect: `https://script.google.com/macros/s/AKfycbyFo0RnixkbDzmvVLqH9PuEotSmYh3wvyJMTAyzGsa8ki-asIK1caQhutUT4fs4EKY/exec?af_r=...`

---

## 🔑 Key Findings

- **Malware downloads secondary payloads silently**
- **Installs WinDivert kernel driver** for packet interception
- **Hijacks .com file extensions** to redirect execution
- Adds **Windows Defender exclusions**
- Establishes persistence through **Windows Scheduled Tasks**

---

## 📂 Repository Content

| Folder/File | Description |
|-------------|-------------|
| [`report.pdf`](./report.pdf) | Complete, detailed analysis report |
| [`dll_source/`](./dll_source/) | Decompiled malicious DLL code |
| [`scripts/`](./scripts/) | Malicious script payloads (`api.php`) |
| [`registry/`](./registry/) | Registry changes captured by Regshot |
| [`traffic/`](./traffic/) | Network captures (Wireshark PCAP) |
| [`screenshots/`](./screenshots/) | Screenshots and evidence |
| [`VirusTotal/`](./VirusTotal/) | VirusTotal reports & exports |

---

## 🛠️ Tools Used

- **Wireshark**
- **Process Monitor**
- **Regshot**
- **dnSpy**
- **FakeNet-NG**
- **VirusTotal**

---

## 🧩 Indicators of Compromise (IOCs)

### Suspicious IP Addresses:
69.62.119.2
69.62.119.17
### Malicious Domains:
solarcellled.com 
blgzd.com 
chrome.homepagel.com 
srv760637.hstgr.cloud
### Scheduled Task:
BackupWinTask
---
## 📝 Lessons Learned

- Trust official download links only.
- Zero antivirus detections ≠ safety.
- Sponsored ads can host malware.
- Regularly review scheduled tasks and registry changes.

---

## 🚨 Disclaimer

**This repository is for educational and research purposes only.**  
Never execute malware outside a secure sandbox or virtual machine.

---

## 🔖 Contributing & Contact

Contributions are welcome! If you discover additional info, open an issue or a pull request.

Stay Safe! 
