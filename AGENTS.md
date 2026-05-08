## Test execution

When running specific tests, use Microsoft Testing Platform (MTP) tree node filters.

Do **not** use VSTest filters:

```bash
dotnet test --filter ...
```

Use `--project` to target the test project, then pass MTP arguments after `--`:

```bash
dotnet test --project <project-directory-or-csproj> -- --treenode-filter "<filter>"
```

Filter format:

```text
/<Assembly>/<Namespace>/<Class>/<Test>
```

Examples:

```bash
dotnet test --project ./tests/MyProject.Tests -- --treenode-filter "/*/*/*/MyTest"

dotnet test --project ./tests/MyProject.Tests -- --treenode-filter "/*/*/MyTestClass/*"
```
