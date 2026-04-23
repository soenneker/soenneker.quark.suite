## Test execution

If you need to execute specific tests, use Microsoft Testing Platform (MTP) filters, not VSTest filters.

Do not use:

```bash
dotnet test --filter ...
````

Use:

```bash
dotnet test -- --treenode-filter "<filter>"
```

Format:

```text
/<Assembly>/<Namespace>/<Class>/<Test>
```

Examples:

```bash
dotnet test -- --treenode-filter "/*/*/*/MyTest"
dotnet test -- --treenode-filter "/*/*/MyTestClass/*"
```