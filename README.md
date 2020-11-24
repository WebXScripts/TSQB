# TSQB

TSQB is my amateur project to create a fully asynchronous bot for TeamSpeak3 servers. The project is still under development and any bugs should be reported in issues. Feel free to contribute!


### Pros

  - Support for TeamSpeak events
  - Fast and safe
  - Multi-platform (.net core)
 

### Tech

TSQB uses a number of open source projects to work properly:

* [TeamSpeak3QueryAPI](https://github.com/nikeee/TeamSpeak3QueryAPI) - .NET wrapper for the TeamSpeak 3 Query API
* [NLog](https://github.com/NLog/NLog) - Advanced and Structured Logging for Various .NET Platforms
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) -  popular high-performance JSON framework for .NET

### Installation

TSQB requires .NET Core 3.1.

I planned to write the installation after the official release.

### Bot Startup

To run the bot, use the command:

```ssh
$ dotnet TSQB.dll <args>
```

Use arguments to specify bot settings:

| Argument | Default Value | Description |
| ------ | ------ | ------ |
| --ip | localhost | TeamSpeak IPv4 Address |
| --port | 10011 | TeamSpeak Query port |
| --id | 1 | TeamSpeak server id |
| --login | serveradmin | Serveradmin login |
| --password | foobar | Serveradmin password |
| --nickname | TSQB @ Bot | Bot name |

Example:

```ssh
$ dotnet TSQB.dll --ip 192.168.1.5 --password superpassword --nickname GLaDoS
```

### [Todos (Please note that the bot is NOT fully finished yet.)](https://github.com/WebXScripts/TSQB/issues/1)
### License
MIT

