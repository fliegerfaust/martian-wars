using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
  [UsedImplicitly]
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataLevelsPath = "StaticData/Levels";
    private const string StaticDataPlayerPath = "StaticData/Player";

    private PlayerStaticData _player;
    private Dictionary<string, LevelStaticData> _levels;

    public void Load()
    {
      LoadLevels();
      LoadPlayer();
    }

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

    public PlayerStaticData ForPlayer() =>
      _player;

    private void LoadPlayer() =>
      _player = Resources.Load<PlayerStaticData>(StaticDataPlayerPath);

    private void LoadLevels()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(StaticDataLevelsPath)
        .ToDictionary(x => x.LevelKey, x => x);
    }
  }
}