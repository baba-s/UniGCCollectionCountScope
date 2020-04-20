# Uni GC Collection Count Scope

GC の発生回数を計測するクラス  

## 使用例

```cs
GCCollectionCountScope.OnStart += name =>
{
    Debug.Log( $"[GCCollectionCount]「{name}」開始" );
};
GCCollectionCountScope.OnComplete += ( name, count ) =>
{
    Debug.Log( $"[GCCollectionCount]「{name}」終了    {count} 回" );
};

using ( new GCCollectionCountScope( "【ここにタグ名】" ) )
{
}
```