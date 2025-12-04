using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;

#if UNITY_2021_2_OR_NEWER
using UnityEditor.SceneManagement;
#else
using UnityEditor.Experimental.SceneManagement;
#endif
#endif

namespace Mirror
{
    
    /// <summary>NetworkIdentity identifies objects across the network.</summary>
    [DisallowMultipleComponent]
    // NetworkIdentity.Awake initializes all NetworkComponents.
    // let's make sure it's always called before their Awake's.
    [DefaultExecutionOrder(-1)]
    [AddComponentMenu("Network/Network Identity")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-identity")]
    public sealed class NetworkIdentity : MonoBehaviour
    {
        

        /// <summary>Unique identifier for NetworkIdentity objects within a scene, used for spawning scene objects.</summary>
        // persistent scene id <sceneHash/32,sceneId/32> (see AssignSceneID comments)
        [FormerlySerializedAs("m_SceneId"), HideInInspector]
        public ulong sceneId;

        // assetId used to spawn prefabs across the network.
        // originally a Guid, but a 4 byte uint is sufficient
        // (as suggested by james)
        //
        // it's also easier to work with for serialization etc.
        // serialized and visible in inspector for easier debugging
        [SerializeField] uint _assetId;

        // The AssetId trick:
        //   Ideally we would have a serialized 'Guid m_AssetId' but Unity can't
        //   serialize it because Guid's internal bytes are private
        //
        //   Using just the Guid string would work, but it's 32 chars long and
        //   would then be sent over the network as 64 instead of 16 bytes
        //
        // => The solution is to serialize the string internally here and then
        //    use the real 'Guid' type for everything else via .assetId
        public uint assetId
        {
            get
            {
#if UNITY_EDITOR
                // old UNET comment:
                // This is important because sometimes OnValidate does not run
                // (like when adding NetworkIdentity to prefab with no child links)
                if (_assetId == 0)
                    SetupIDs();
#endif
                return _assetId;
            }
            // assetId is set internally when creating or duplicating a prefab
            internal set
            {
                // should never be empty
                if (value == 0)
                {
                    Debug.LogError($"Can not set AssetId to empty guid on NetworkIdentity '{name}', old assetId '{_assetId}'");
                    return;
                }

                // always set it otherwise.
                // for new prefabs,        it will set from 0 to N.
                // for duplicated prefabs, it will set from N to M.
                // either way, it's always set to a valid GUID.
                _assetId = value;
                // Debug.Log($"Setting AssetId on NetworkIdentity '{name}', new assetId '{value:X4}'");
            }
        }


        // hasSpawned should always be false before runtime
        [SerializeField, HideInInspector] bool hasSpawned;
        


        

        void OnValidate()
        {
            // OnValidate is not called when using Instantiate, so we can use
            // it to make sure that hasSpawned is false
            hasSpawned = false;

#if UNITY_EDITOR
            
            SetupIDs();
#endif
        }

        // expose our AssetId Guid to uint mapping code in case projects need to map Guids to uint as well.
        // this way their projects won't break if we change our mapping algorithm.
        // needs to be available at runtime / builds, don't wrap in #if UNITY_EDITOR
        public static uint AssetGuidToUint(Guid guid) => (uint)guid.GetHashCode(); // deterministic


#if UNITY_EDITOR
        void AssignAssetID(string path)
        {
            // only set if not empty. fixes https://github.com/vis2k/Mirror/issues/2765
            if (!string.IsNullOrWhiteSpace(path))
            {
                // if we generate the assetId then we MUST be sure to set dirty
                // in order to save the prefab object properly. otherwise it
                // would be regenerated every time we reopen the prefab.
                // -> Undo.RecordObject is the new EditorUtility.SetDirty!
                // -> we need to call it before changing.
                //
                // to verify this, duplicate a prefab and double click to open it.
                // add a log message if "_assetId != before_".
                // without RecordObject, it'll log every time because it's not saved.
                Undo.RecordObject(this, "Assigned AssetId");

                // uint before = _assetId;
                Guid guid = new Guid(AssetDatabase.AssetPathToGUID(path));
                assetId = AssetGuidToUint(guid);
                // if (_assetId != before) Debug.Log($"Assigned assetId={assetId} to {name}");
            }
        }

        void AssignAssetID(GameObject prefab) => AssignAssetID(AssetDatabase.GetAssetPath(prefab));

        
        void SetupIDs()
        {
            
                // force 0 for prefabs
                sceneId = 0;
                AssignAssetID(gameObject);
           
        }

#endif
    }

}
