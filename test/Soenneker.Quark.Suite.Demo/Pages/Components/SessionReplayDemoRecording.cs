using System.Text.Json;

namespace Soenneker.Quark.Suite.Demo.Pages.Components;

internal static class SessionReplayDemoRecording
{
    public static JsonElement[] Create() => JsonSerializer.Deserialize<JsonElement[]>("""
        [
          {"type":4,"timestamp":1700000000000,"data":{"href":"https://example.com/projects","width":960,"height":540}},
          {"type":2,"timestamp":1700000000001,"data":{"initialOffset":{"top":0,"left":0},"node":{
            "type":0,"id":1,"childNodes":[
              {"type":1,"id":2,"name":"html","publicId":"","systemId":""},
              {"type":2,"id":3,"tagName":"html","attributes":{"lang":"en"},"childNodes":[
                {"type":2,"id":4,"tagName":"head","attributes":{},"childNodes":[
                  {"type":2,"id":5,"tagName":"style","attributes":{},"childNodes":[
                    {"type":3,"id":6,"textContent":"*{box-sizing:border-box}body{margin:0;background:#fff;color:#25272b;font:14px system-ui,sans-serif}header{display:flex;align-items:center;justify-content:space-between;padding:24px 36px;border-bottom:1px solid #eeeff1}.brand{font-size:16px;font-weight:650;letter-spacing:-.5px}.avatar{display:grid;place-items:center;width:28px;height:28px;border-radius:50%;background:#f1f2f4;color:#777;font-size:10px}main{padding:40px 64px}.eyebrow{font-size:10px;letter-spacing:1.5px;text-transform:uppercase;color:#96999f}h1{font-size:28px;letter-spacing:-1px;margin:12px 0 8px;font-weight:550}p{color:#858991;margin:0 0 30px;line-height:1.7}.row{display:flex;align-items:center;justify-content:space-between;padding:18px 0;border-bottom:1px solid #eff0f2}.name{font-size:13px;font-weight:500}.detail{color:#9a9ea5;font-size:11px;margin-top:4px}.status{background:#f2f6f3;color:#65836d;border-radius:20px;padding:5px 10px;font-size:10px}.draft{background:#f4f4f5;color:#8d9097}footer{display:flex;justify-content:space-between;margin-top:28px;color:#9a9ea5;font-size:11px}button{border:0;border-radius:6px;background:#25272b;color:white;padding:9px 13px;font:11px system-ui}"}
                  ]}
                ]},
                {"type":2,"id":7,"tagName":"body","attributes":{},"childNodes":[
                  {"type":2,"id":8,"tagName":"header","attributes":{},"childNodes":[
                    {"type":2,"id":9,"tagName":"div","attributes":{"class":"brand"},"childNodes":[{"type":3,"id":10,"textContent":"Studio"}]},
                    {"type":2,"id":11,"tagName":"div","attributes":{"class":"avatar"},"childNodes":[{"type":3,"id":12,"textContent":"JD"}]}
                  ]},
                  {"type":2,"id":13,"tagName":"main","attributes":{},"childNodes":[
                    {"type":2,"id":14,"tagName":"div","attributes":{"class":"eyebrow"},"childNodes":[{"type":3,"id":15,"textContent":"Workspace"}]},
                    {"type":2,"id":16,"tagName":"h1","attributes":{},"childNodes":[{"type":3,"id":17,"textContent":"Your projects"}]},
                    {"type":2,"id":18,"tagName":"p","attributes":{},"childNodes":[{"type":3,"id":19,"textContent":"A little space for your next big idea."}]},
                    {"type":2,"id":20,"tagName":"div","attributes":{"class":"row"},"childNodes":[
                      {"type":2,"id":21,"tagName":"div","attributes":{"class":"name"},"childNodes":[{"type":3,"id":22,"textContent":"Website refresh"}]},
                      {"type":2,"id":23,"tagName":"span","attributes":{"class":"status"},"childNodes":[{"type":3,"id":24,"textContent":"In progress"}]}
                    ]},
                    {"type":2,"id":25,"tagName":"div","attributes":{"class":"row"},"childNodes":[
                      {"type":2,"id":26,"tagName":"div","attributes":{"class":"name"},"childNodes":[{"type":3,"id":27,"textContent":"Brand guidelines"}]},
                      {"type":2,"id":28,"tagName":"span","attributes":{"class":"status draft"},"childNodes":[{"type":3,"id":29,"textContent":"Draft"}]}
                    ]},
                    {"type":2,"id":30,"tagName":"footer","attributes":{},"childNodes":[
                      {"type":2,"id":31,"tagName":"span","attributes":{},"childNodes":[{"type":3,"id":32,"textContent":"2 projects"}]},
                      {"type":2,"id":33,"tagName":"button","attributes":{},"childNodes":[{"type":3,"id":34,"textContent":"New project"}]}
                    ]}
                  ]}
                ]}
              ]}
            ]
          }}},
          {"type":3,"timestamp":1700000003000,"data":{"source":1,"positions":[{"x":320,"y":285,"id":21,"timeOffset":0}]}},
          {"type":3,"timestamp":1700000006500,"data":{"source":0,"texts":[{"id":24,"value":"In review"}],"attributes":[],"removes":[],"adds":[]}},
          {"type":3,"timestamp":1700000009500,"data":{"source":1,"positions":[{"x":580,"y":340,"id":28,"timeOffset":0}]}},
          {"type":3,"timestamp":1700000012000,"data":{"source":0,"texts":[{"id":29,"value":"In progress"}],"attributes":[{"id":28,"attributes":{"class":"status"}}],"removes":[],"adds":[]}},
          {"type":3,"timestamp":1700000016000,"data":{"source":1,"positions":[{"x":830,"y":418,"id":33,"timeOffset":0}]}}
        ]
        """)!;
}
