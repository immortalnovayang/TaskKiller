# TaskKiller 功能
啟動後過特定秒數，強制關閉(Taskkill)指定程式，用快捷鍵F1可再開啟(可關閉桌面)

# 修改設定

App.config內可以修改


```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <!--啟動後延遲秒數-->
    <add key="delay" value="60" />
    <!--時間到後要結束的程式名稱-->
    <add key="taskname" value="explorer.exe" />
    <add key="ClientSettingsProvider.ServiceUri" value="" />
  </appSettings>
</configuration>
```


此程式執行後，過60秒會結束explorer.exe (這個例子為關閉桌面)，可替換其他程式

按F1會再重新開啟該程式

按F2會再結束該程式

※目前還沒自訂快捷鍵功能
