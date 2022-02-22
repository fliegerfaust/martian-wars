using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssets
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
  }
}