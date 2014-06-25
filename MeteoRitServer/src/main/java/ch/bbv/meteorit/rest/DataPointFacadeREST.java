package ch.bbv.meteorit.rest;

import javax.ejb.Stateless;
import javax.inject.Inject;
import javax.ws.rs.Consumes;
import javax.ws.rs.POST;
import javax.ws.rs.Path;

import ch.bbv.meteorit.bean.DataPoint;
import ch.bbv.meteorit.persistence.db.DBPersistence;

@Stateless
@Path("datapoint")
public class DataPointFacadeREST {

	@Inject
	DBPersistence persistence;

	@POST
	@Consumes({ "application/json" })
	public void create(DataPoint entity) {
		persistence.updateMeasurement(entity);
	}
}
