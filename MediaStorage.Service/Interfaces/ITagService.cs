using System.Collections.Generic;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Tag;

namespace MediaStorage.Service
{
    public interface ITagService
    {
        ServiceResult AddTag(TagViewModel entity);
        List<TagViewModel> GetAllTags();
        TagViewModel GetTagById(int id);
        ServiceResult RemoveTag(int id);
        ServiceResult UpdateTag(TagViewModel entity);
    }
}