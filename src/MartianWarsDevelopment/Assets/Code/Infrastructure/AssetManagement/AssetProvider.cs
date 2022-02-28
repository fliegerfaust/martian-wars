using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssets
  {
    public GameObject Instantiate(string path)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
      GameObject prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
  }
}