﻿#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 自动绑定全局设置
/// </summary>
public class AutoBindSettingConfig : ScriptableObject
{
    private const string DefaultStr = "Default";
    [SerializeField]
    private List<AutoBindSettingData> m_Settings = new List<AutoBindSettingData>();
    public List<AutoBindSettingData> Settings
    {
        get => m_Settings;
    }

    public AutoBindSettingData Default
    {
        get
        {
            var data = GetSettingData(DefaultStr);
            if (data == null)
            {
                data = new AutoBindSettingData(DefaultStr);
                m_Settings.Add(data);
            }

            return data;
        }
    }

    public AutoBindSettingData GetSettingData(string settingName)
    {
        int index = m_Settings.FindIndex(_ => _.Name == settingName);
        if (index == -1)
        {
            return null;
        }
        return m_Settings[index];
    }
    public bool AddSettingData(AutoBindSettingData data)
    {
        int index = m_Settings.FindIndex(_ => _.Name == data.Name);
        if (index == -1)
        {
            m_Settings.Add(data);
            return true;
        }
        return false;
    }

    [MenuItem("Tools/AutoBindTools/CreateAutoBindSettingConfig")]
    public static void CreateAutoBindSettingConfig()
    {
        string[] paths = AssetDatabase.FindAssets("t:AutoBindSettingConfig");
        if (paths.Length >= 1)
        {
            string path = AssetDatabase.GUIDToAssetPath(paths[0]);
            EditorUtility.DisplayDialog("警告", $"已存在AutoBindSettingConfig，路径:{path}", "确认");
            return;
        }
        AutoBindSettingConfig settingConfig = CreateInstance<AutoBindSettingConfig>();
        settingConfig. m_Settings.Add(new AutoBindSettingData(DefaultStr));

        AssetDatabase.CreateAsset(settingConfig, "Assets/AutoBindSettingConfig.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif