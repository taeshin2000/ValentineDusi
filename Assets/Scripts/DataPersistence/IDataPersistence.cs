using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence {
    void LoadData(WordImages wordImages);
    void SaveData(ref WordImages wordImages);

}

