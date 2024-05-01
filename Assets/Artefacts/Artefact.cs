using UnityEngine;

namespace Scenes
{
    public enum ArtefactName
    {
        Caltulator,
        Differential,
    }
    public class Artefact : MonoBehaviour
    {
        public ArtefactName type;
        public bool spawnOnStart;
    }
}