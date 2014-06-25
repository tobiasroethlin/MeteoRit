package ch.bbv.meteorit.rest;

import java.util.List;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;

import ch.bbv.meteorit.entities.City;

@Stateless
@Path("city")
public class CityFacadeREST {

	@PersistenceContext
	private EntityManager em;

	@POST
	@Consumes({ "application/json" })
	public void create(City entity) {
		em.persist(entity);
	}

	@PUT
	@Consumes({ "application/json" })
	public void update(City entity) {
		em.merge(entity);
	}
	
	@GET
	@Path("{id}")
	@Produces({ "application/json" })
	public City find(@PathParam("id") Integer id) {
		return em.find(City.class, id);
	}

	@GET
	@Path("bySensorId/{sensorId}")
	@Produces({ "application/json" })
	public City findBySensorId(@PathParam("sensorId") Integer sensorId) {
		List<City> cities = em
				.createNamedQuery("City.findBySensorId", City.class)
				.setParameter("sensorId", sensorId).getResultList();
		if (!cities.isEmpty()) {
			return cities.get(0);
		}
		return new City();
	}

	@GET
	@Path("all")
	@Produces({ "application/json" })
	public List<City> getAll() {
		List<City> cities = em.createNamedQuery("City.findAll", City.class)
				.getResultList();
		return cities;
	}
}