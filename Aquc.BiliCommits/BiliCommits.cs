using System.Text.Json.Nodes;

namespace Aquc.BiliCommits;

public class BiliCommitsClass
{
    public static async Task<string> GetReply(HttpClient http, string commitId, int index = 0)
    {
        var response = await http.GetAsync($"https://api.bilibili.com/x/v2/reply/main?mode=2&oid={commitId}&pagination_str=%7B%22offset%22:%22%22%7D&plat=1&seek_rpid=0&type=17");
        //ncpe
        var replies = JsonNode.Parse(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync())!["data"]!["replies"]!.AsArray();
        if (replies.Count > index)
            return replies[index]!["content"]!["message"]!.ToString().Replace("%",".");
        else
            throw new IndexOutOfRangeException();
    }
    public static async Task<string> GetReply(string commitId, int index = 0)
    {
        using var http = new HttpClient();
        return await GetReply(http, commitId, index);
    }
}