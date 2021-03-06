package ch.bbv.meteorit.rest;

import javax.ejb.Stateless;
import javax.inject.Inject;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;

import ch.bbv.meteorit.entities.Measurement;
import ch.bbv.meteorit.persistence.db.DBPersistence;

@Stateless
@Path("measurement")
public class MeasurementFacadeREST {

	@Inject
	private DBPersistence persistence;

	@GET
	@Path("{id}/{timestamp}")
	@Produces({ "application/json" })
	public Measurement find(@PathParam("id") Integer id, @PathParam("timestamp") long timestamp) {
		return persistence.getMeasurement(id, timestamp);
	}

	@GET
	@Path("{id}")
	@Produces({ "application/json" })
	public Measurement find(@PathParam("id") Integer id) {
		return persistence.getMeasurement(id);
	}
}
