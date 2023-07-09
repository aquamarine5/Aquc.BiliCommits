using System.Text.Json.Nodes;

namespace Aquc.BiliCommits;

public class BiliCommits
{
    public static async Task<string> GetReply(string commitId,int index = 0)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://api.bilibili.com/x/v2/reply/main?jsonp=jsonp&next=0&type=11&oid={commitId}&mode=2&plat=1");
        var replies = JsonNode.Parse(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync())!["data"]!["replies"]!.AsArray();
        http.Dispose();
        if (replies.Count > index)
            return replies[index]!["content"]!["message"]!.ToString();
        else
            throw new IndexOutOfRangeException();
    }
}