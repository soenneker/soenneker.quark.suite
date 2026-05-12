## Fluent Builder Chains

When using fluent builders, read the chain strictly left to right. A fluent modifier configures the next concrete value that appears after it. Do not assume a modifier applies backward to the value before it.

Prefer this shape:

`Builder.Modifier.Value.Modifier.Value`

Avoid this shape when the modifier is intended to affect the earlier value:

`Builder.Value.Modifier`

Before changing or adding builder chains, verify which value each modifier binds to by reading left to right.


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
