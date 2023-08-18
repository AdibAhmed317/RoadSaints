using RoadSaintsAPI.DB_Config;
using RoadSaintsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RoadSaintsAPI.Repository
{
    public class CustomersRepo
    {
        public bool AddCustomer(CustomersModel customer)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var newCustomer = new Customers
                {
                    customer_id = customer.CustomerId,
                    customer_name = customer.CustomerName,
                    email = customer.Email,
                    password = customer.Password,
                    address = customer.Address,
                    phone = customer.Phone,
                    isAdmin = customer.IsAdmin,
                };

                context.Customers.Add(newCustomer);
                int affectedRows = context.SaveChanges();

                return affectedRows > 0;
            }
        }

        public List<CustomersModel> GetAllCustomers()
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var result = context.Customers.Select(x => new CustomersModel()
                {
                    CustomerId = x.customer_id,
                    CustomerName = x.customer_name,
                    Email = x.email,
                    Password = x.password,
                    Address = x.address,
                    Phone = x.phone,
                    IsAdmin = x.isAdmin,
                }).ToList();

                return result;
            }
        }

        public CustomersModel GetCustomerById(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var customerEntity = context.Customers.FirstOrDefault(c => c.customer_id == customerId);
                if (customerEntity == null)
                {
                    return null;
                }

                var customerModel = new CustomersModel
                {
                    CustomerId = customerEntity.customer_id,
                    CustomerName = customerEntity.customer_name,
                    Email = customerEntity.email,
                    Password = customerEntity.password,
                    Address = customerEntity.address,
                    Phone = customerEntity.phone,
                    IsAdmin = customerEntity.isAdmin,
                };

                return customerModel;
            }
        }

        public bool UpdateCustomerById(int customerId, CustomersModel customer)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var existingCustomer = context.Customers.FirstOrDefault(c => c.customer_id == customerId);
                if (existingCustomer != null)
                {
                    existingCustomer.customer_name = customer.CustomerName;
                    existingCustomer.email = customer.Email;
                    existingCustomer.password = customer.Password;
                    existingCustomer.address = customer.Address;
                    existingCustomer.phone = customer.Phone;
                    existingCustomer.isAdmin = customer.IsAdmin;

                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteCustomerById(int customerId)
        {
            using (var context = new Bike_AccessoriesEntities())
            {
                var existingCustomer = context.Customers.FirstOrDefault(c => c.customer_id == customerId);
                if (existingCustomer != null)
                {
                    context.Customers.Remove(existingCustomer);
                    int affectedRows = context.SaveChanges();

                    return affectedRows > 0;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}