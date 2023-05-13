using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence {
    void LoadData(GameData wordImages);
    void SaveData(ref GameData wordImages);

}

