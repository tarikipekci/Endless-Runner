using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterInfo/Character")]
public class CharacterInfo : ScriptableObject
{
    public Sprite characterIcon;
    public int characterPrice;
    [SerializeField] private string guid;
    [SerializeField] private bool _isPurchased;
    
    public bool purchased
    {
        get => PlayerPrefs.GetInt(GetCompletedKey(), 0) == 1;
        set
        {
            _isPurchased = value;
            PlayerPrefs.SetInt(GetCompletedKey(), value ? 1 : 0);
        }
    }
    
    private string GetCompletedKey() => $"{guid}_IsPurchased";
    
    private void OnEnable()
    {
        _isPurchased = purchased;
        if (string.IsNullOrEmpty(guid))
        {
            guid = System.Guid.NewGuid().ToString();
            Debug.Log($"New GUID generated for {name}: {guid}");

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}
