# jsondiffpatch.net

fork 自 [wbish/jsondiffpatch.net: JSON object diffs and reversible patching (jsondiffpatch compatible)](https://github.com/wbish/jsondiffpatch.net )

对应的前端项目：
[benjamine/jsondiffpatch: Diff & patch JavaScript objects](https://github.com/benjamine/jsondiffpatch )

## 内部 NUGET 包

JsonDiffPatch.Net.Local

`<PackageReference Include="JsonDiffPatch.Net.Local" Version="2.4.0-alpha02" />`

打包方式：推 tag 打包

## 修改点

### 1 让数组的列表 Item 项的对比，支持 Index 参数

[add index param for array item match (3575fc76) · Commits · dotnet / jsondiffpatch.net · GitLab](https://gitlab.gz.cvte.cn/iip-win/jsondiffpatch.net/-/commit/3575fc76a75e2314e70399cd49da1aee9daddc00 )

### 2 输出 patch 数据时，不记录旧数据

[不在 patch 中记录旧数据（不考虑撤销操作） (ed4a7f10) · Commits · dotnet / jsondiffpatch.net · GitLab](https://gitlab.gz.cvte.cn/iip-win/jsondiffpatch.net/-/commit/ed4a7f10e3ab091e80ea1ccd482519f96e9d6f31 )
