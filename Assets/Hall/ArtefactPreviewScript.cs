using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactPreviewScript : MonoBehaviour
{
    public GameObject ArtefactPreview;
    public ArtefactPath[] ArtefactPaths;

    private Dictionary<string, GameObject> _artefacts;

    private void Start()
    {
        _artefacts = new Dictionary<string, GameObject>();
        foreach (var key in ArtefactStorage.Keys)
            AddArtefact(key);
        ArtefactStorage.ArtefactAdded += ArtefactStorage_ArtefactAdded;
        ArtefactStorage.ArtefactRemoved += ArtefactStorage_ArtefactRemoved;
    }

    private void OnDestroy()
    {
        ArtefactStorage.ArtefactAdded -= ArtefactStorage_ArtefactAdded;
        ArtefactStorage.ArtefactRemoved -= ArtefactStorage_ArtefactRemoved;
    }

    private void ArtefactStorage_ArtefactAdded(string key)
    {
        AddArtefact(key);
    }

    private void ArtefactStorage_ArtefactRemoved(string key)
    {
        if (_artefacts.TryGetValue(key, out var artefact))
        {
            Destroy(artefact);
            _artefacts.Remove(key);
        }
    }

    private void AddArtefact(string name)
    {
        var path = ArtefactPaths.FirstOrDefault(a => a.Name == name);
        if (path == null)
        {
            Debug.LogWarning($"Artefact '{name}' has no path to the preview.");
            return;
        }
        var artefact = Instantiate(ArtefactPreview, gameObject.transform);
        artefact.GetComponent<Image>().sprite = path.Sprite;
        _artefacts[name] = artefact;
    }

    [Serializable]
    public class ArtefactPath
    {
        public string Name;
        public Sprite Sprite;
    }
}

