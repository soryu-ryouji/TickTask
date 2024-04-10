# Tick Task

A command-line task manager similar to Taskwarrior.

## Commands

| Name           | Description                 | Example                                   |
| -------------- | --------------------------- | ----------------------------------------- |
| add            | Add a new task              | tick add Read The Art of Unix Programming |
| ls             | List task                   | tick ls (-a/-all)                         |
| done           | Use index to mark completed | tick 114514 done                          |
| rm / remove    | Remove task                 | tick remove 114514                        |
| -v / --version | show program version        | tick -v                                   |

## Publish

```shell
dotnet publish -r win-x64 -c Release
```