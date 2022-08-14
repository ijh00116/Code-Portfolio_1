using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : Monosingleton<ResourcesLoader>
{
    public class LoadedResource
    {
        public Object resource;
        public int referencedCount;
        public LoadedResource(Object resource)
        {
            this.resource = resource;
            this.referencedCount = 1;
        }
    }

    Dictionary<string, LoadedResource> loadedResources = new Dictionary<string, LoadedResource>();
    Dictionary<string, ResourceRequest> inProgressOperations = new Dictionary<string, ResourceRequest>();
   
    public IEnumerator Load<T>(string ResourceName,System.Action<Object> onComplete)
    {
        while (inProgressOperations.ContainsKey(ResourceName))
            yield return null;

        if(loadedResources.ContainsKey(ResourceName))
        {
            var resource=loadedResources[ResourceName];
            if(resource!=null&&resource.resource!=null)
            {
                resource.referencedCount++;
                if (onComplete != null)
                    onComplete(resource.resource);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Resource Is Already Loaded. But Actual Data Not Loaded");
#endif
            }
        }
        else
        {
            ResourceRequest request = Resources.LoadAsync(ResourceName, typeof(T));
            inProgressOperations.Add(ResourceName, request);

            yield return request;

            inProgressOperations.Remove(ResourceName);

            if(request.asset!=null)
            {
                LoadedResource lr = new LoadedResource(request.asset);
                loadedResources.Add(ResourceName, lr);
            }
            
            if(onComplete != null)
            {
                onComplete?.Invoke(request.asset);
            }
        }
    }
    public void UnloadAll()
    {
        loadedResources.Clear();
        Resources.UnloadUnusedAssets();
    }
}
