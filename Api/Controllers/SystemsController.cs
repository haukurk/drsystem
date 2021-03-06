﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Filters;
using Api.Models;
using Data;
using Data.Models;

namespace Api.Controllers
{
    public class SystemsController : BaseApiController
    {

        public SystemsController(IDRSRepository repo)
            : base(repo)
        {

        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpGet]
        public IEnumerable<SystemModel> Get()
        {

            IQueryable<DRSSystem> query;

            query = DRSRepository.GetAllSystems();

            var results = query.ToList().Select(s => DRSModelFactory.Create(s));

            return results;
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpGet]
        public HttpResponseMessage GetSystem(int id)
        {

            try
            {
                var system = DRSRepository.GetSystem(id);
                if (system != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(system));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] SystemModel systemModel)
        {
            try
            {
                var entity = DRSModelFactory.Parse(systemModel);

                if (entity == null)
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read system from body.");

                if (DRSRepository.Insert(entity) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, DRSModelFactory.Create(entity));
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the core database.");
                
            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.EntityValidationErrors.First().ValidationErrors);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] SystemModel fromModel)
        {
            try
            {
                var updatedSystem = DRSModelFactory.Parse(fromModel);

                if (updatedSystem == null)
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read system from body.");

                var originalSystem = DRSRepository.GetSystem(id);

                if (originalSystem == null || originalSystem.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "System is not found.");
                }
                
                updatedSystem.Id = id;
                
                if (DRSRepository.Update(originalSystem, updatedSystem) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(updatedSystem));
                }

                return Request.CreateResponse(HttpStatusCode.NotModified);

            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.EntityValidationErrors.First().ValidationErrors);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var system = DRSRepository.GetSystem(id);

                if (system == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                if (system.ReviewEntities != null && system.ReviewEntities.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Can not detele system, some review entites has it as a system.");

                if (DRSRepository.DeleteSystem(id) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
