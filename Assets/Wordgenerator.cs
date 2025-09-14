using UnityEngine;

public class Wordgenerator : MonoBehaviour
{
    public static string[] wordList = new string[]
    {
       "algorithm",
    "analytics",
    "backend",
    "binary",
    "blockchain",
    "browser",
    "bug",
    "byte",
    "cache",
    "cloud",
    "code",
    "computer",
    "cyber",
    "database",
    "debug",
    "DevOps",
    "digital",
    "domain",
    "encrypt",
    "firewall",
    "frontend",
    "gadget",
    "gigabyte",
    "glitch",
    "hacker",
    "hardware",
    "hyperlink",
    "internet",
    "keyboard",
    "latency",
    "library",
    "malware",
    "network",
    "pixel",
    "platform",
    "protocol",
    "query",
    "router",
    "server",
    "software",
    "stream",
    "syntax",
    "terminal",
    "token",
    "upload",
    "virtual",
    "website",
    "wireless",
    "javascript"
    };

   public static string GetRandomWord()
   {
       int randomIndex = Random.Range(0, wordList.Length);
       string randomWord = wordList[randomIndex];
       return randomWord;
   }


}
