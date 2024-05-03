using UnityEngine;

namespace Scenes
{
    public enum ArtefactName
    {
        Calculator,
        Differential,
    }

    public class Artefact : MonoBehaviour
    {
        public ArtefactName type;
        public bool spawnOnStart;
    }
}