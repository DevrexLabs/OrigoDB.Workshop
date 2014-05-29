#r ..\Twitter.Core.dll

using OrigoDB.Core;
using Twitter.Core;

var t = Db.For<TwitterVerse>();

t.AddUser("robtheslob");
t.AddUser("neo");
t.Follow("neo", "robtheslob");
t.Tweet("neo", "What is the Matrix?", DateTime.Now);


