using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using AppCore;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models.WebAPIModels;

namespace MvcApplication.Controllers
{
    public class TagWebAPIController : ApiController
    {
        private readonly CoreHolder core;

        public TagWebAPIController() : base() { core = new CoreHolder(); }

        //1. Get a list of all tags	GET	/api/tags
        public ICollection<TagModel> GetAllTags()
        {
            try
            {
                List<TagModel> list = new List<TagModel>();
                foreach (var tag in core.TagRepository.ReadAll())
                {
                    var tag_model = new TagModel();
                    tag_model.ID = tag.ID;
                    tag_model.Name = tag.Name;
                    tag_model.Pictures = tag.Pictures;
                    list.Add(tag_model);
                }
                return list;
            }
            catch { return null; }
        }

        //2. Get a tag by ID	GET	/api/tags/id
        public TagModel GetTagByID(int id)
        {
            var tag = core.TagRepository.Read(id);
            if (tag == null) return null;
            return new TagModel()
            {
                ID = tag.ID,
                Name = tag.Name,
                Pictures = tag.Pictures,
            };
        }

        //3. Get tags by picture    GET	/api/tags?picture_id=picture_id
        public ICollection<TagModel> GetTagsByPictureID(int picture_id)
        {
            try
            {
                List<TagModel> list = new List<TagModel>();
                foreach (var tag in core.PictureRepository.GetTags(picture_id))
                {
                    var tag_model = new TagModel();
                    tag_model.ID = tag.ID;
                    tag_model.Name = tag.Name;
                    tag_model.Pictures = tag.Pictures;
                    list.Add(tag_model);
                }
                return list;
            }
            catch { return null; }
        }

        //4. Get tags by user    GET /api/tags?user_id=user_id
        public ICollection<TagModel> GetTagsByUserID(int user_id)
        {
            try
            {
                List<TagModel> list = new List<TagModel>();
                foreach (var tag in core.ProfileRepository.GetTags(user_id))
                {
                    var tag_model = new TagModel();
                    tag_model.ID = tag.ID;
                    tag_model.Name = tag.Name;
                    tag_model.Pictures = tag.Pictures;
                    list.Add(tag_model);
                }
                return list;
            }
            catch { return null; }
        }

        //5. Get tags by picture set    GET /api/tags?picture_set_id=picture_set_id
        public ICollection<TagModel> GetTagsByPictureSetID(int picture_set_id)
        {
            try
            {
                List<TagModel> list = new List<TagModel>();
                foreach (var tag in core.PictureSetRepository.GetTags(picture_set_id))
                {
                    var tag_model = new TagModel();
                    tag_model.ID = tag.ID;
                    tag_model.Name = tag.Name;
                    tag_model.Pictures = tag.Pictures;
                    list.Add(tag_model);
                }
                return list;
            }
            catch { return null; }
        }

        //6. Create tag     POST /api/tags
        public HttpResponseMessage PostTag(TagModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    core.TagRepository.Create(item.Name);
                    core.Submit();
                    var created_tag = core.TagRepository.Find(item.Name);
                    item.ID = created_tag.ID;
                    item.Pictures = created_tag.Pictures;
                    var response = Request.CreateResponse<TagModel>(HttpStatusCode.Created, item);
                    string uri = Url.Link("TagApi", new { id = item.ID });
                    response.Headers.Location = new Uri(uri);
                    return response;
                }
                catch (Exception error) { ModelState.AddModelError("", error.Message); return Request.CreateErrorResponse(HttpStatusCode.Conflict, ModelState); }
            }
            else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //7. Update tag by id    PUT /api/tags/id
        public HttpResponseMessage PutTag(int id, TagModel tag)
        {
            var tag2update = core.TagRepository.Read(id);
            if (tag2update == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            if (ModelState.IsValid)
            {
                try
                {
                    var updated_tag = new Tag(tag.Name);
                    updated_tag.ID = tag2update.ID;
                    core.TagRepository.Update(updated_tag);
                    core.Submit();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch { return new HttpResponseMessage(HttpStatusCode.Conflict); }
            }
            else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //8. Update tag by name    PUT /api/tags?tag_name = tag_name
        public HttpResponseMessage PutTag(string tag_name, TagModel tag)
        {
            var tag2update = core.TagRepository.Find(tag_name);
            if (tag2update == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            if (ModelState.IsValid)
            {
                try
                {
                    var updated_tag = new Tag(tag.Name);
                    updated_tag.ID = tag2update.ID;
                    core.TagRepository.Update(updated_tag);
                    core.Submit();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch { return new HttpResponseMessage(HttpStatusCode.Conflict); }
            }
            else return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //9. Delete tag by id    DELETE /api/tags/id
        public HttpResponseMessage DeleteTag(int id)
        {
            var tag2delete = core.TagRepository.Read(id);
            if (tag2delete == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            try
            {
                core.TagRepository.Delete(id);
                core.Submit();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch { return new HttpResponseMessage(HttpStatusCode.Conflict); }
        }

        //10. Delete tag by name    DELETE /api/tags?tag_name = tag_name
        public HttpResponseMessage DeleteTag(string name)
        {
            var tag2delete = core.TagRepository.Find(name);
            if (tag2delete == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            try
            {
                core.TagRepository.Delete(tag2delete.ID);
                core.Submit();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch { return new HttpResponseMessage(HttpStatusCode.Conflict); }
        }

    }
}
